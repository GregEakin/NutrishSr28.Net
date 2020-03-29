// Copyright 2019 Greg Eakin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using NHibernate;
using SR28lib.Data;
using System.Collections.Generic;
using System.IO;

namespace SR28lib.Parsers
{
    public static class NutData
    {
        public static readonly string DataFile = "..\\..\\..\\data\\NUT_DATA.txt";
        public static readonly string AddData = "..\\..\\..\\data2\\ADD_NUTR.txt";
        public static readonly string ChangeData = "..\\..\\..\\data2\\CHG_NUTR.txt";
        public static readonly string DeleteData = "..\\..\\..\\data2\\DEL_NUTR.txt";

        public static void ParseFile(IStatelessSession session)
        {
            if (File.Exists(DataFile))
            {
                var lines = File.ReadLines(DataFile);
                foreach (var line in lines)
                    AddLine(session, line);
            }

            if (File.Exists(AddData))
            {
                var lines = File.ReadLines(AddData);
                foreach (var line in lines)
                    AddLine(session, line);
            }

            if (File.Exists(ChangeData))
            {
                var lines = File.ReadLines(ChangeData);
                foreach (var line in lines)
                    ChangeLine(session, line);
            }

            if (File.Exists(DeleteData))
            {
                var lines = File.ReadLines(DeleteData);
                foreach (var line in lines)
                    DeleteLine(session, line);
            }
        }

        private static void AddLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var nutrientData = ParseDataSource(session, fields);
            session.Insert(nutrientData);
        }

        private static void ChangeLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var nutrientData = ParseDataSource(session, fields);
            session.Update(nutrientData);
        }

        private static void DeleteLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var nutrientDataKey = ParseDeleteSource(session, fields);
            var nutrientData = session.Get<NutrientData>(nutrientDataKey);
            session.Delete(nutrientData);
        }

        private static NutrientData ParseDataSource(IStatelessSession session, IReadOnlyList<string> fields)
        {
            var item = new NutrientData();

            var NDB_No = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<FoodDescription>(NDB_No);

            var Nutr_No = fields[1].Substring(1, fields[1].Length - 2);
            var nutrientDefinition = session.Get<NutrientDefinition>(Nutr_No);

            item.NutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            item.Nutr_Val = double.Parse(fields[2]);

            item.Num_Data_Pts = int.Parse(fields[3]);

            if (fields[4].Length > 0) item.Std_Error = double.Parse(fields[4]);

            var Src_Cd = fields[5].Substring(1, fields[5].Length - 2);
            item.SourceCode = session.Get<SourceCode>(Src_Cd);
            // item.AddSourceCode(sourceCode);

            if (fields[6].Length > 2)
            {
                var Deriv_Cd = fields[6].Substring(1, fields[6].Length - 2);
                item.DataDerivation = session.Get<DataDerivation>(Deriv_Cd);
                // item.AddDataDerivation(dataDerivation);
            }

            if (fields[7].Length > 2)
            {
                var Ref_NDB_No = fields[7].Substring(1, fields[7].Length - 2);
                var refFoodDescription = session.Get<FoodDescription>(Ref_NDB_No);
                item.FoodDescription = refFoodDescription;
            }

            if (fields[8].Length > 2) item.Add_Nutr_Mark = fields[8].Substring(1, fields[8].Length - 2);
            if (fields[9].Length > 0) item.Num_Studies = int.Parse(fields[9]);
            if (fields[10].Length > 0) item.Min = double.Parse(fields[10]);
            if (fields[11].Length > 0) item.Max = double.Parse(fields[11]);
            if (fields[12].Length > 0) item.DF = int.Parse(fields[12]);
            if (fields[13].Length > 0) item.Low_EB = double.Parse(fields[13]);
            if (fields[14].Length > 0) item.Up_EB = double.Parse(fields[14]);
            if (fields[15].Length > 2) item.Stat_cmt = fields[15].Substring(1, fields[15].Length - 2);
            if (fields[16].Length > 0) item.AddMod_Date = fields[16];
            if (fields[17].Length > 0) item.CC = fields[17];

            //foodDescription.AddNutrientData(item);
            //nutrientDefinition.AddNutrientData(item);

            return item;
        }

        private static NutrientDataKey ParseDeleteSource(IStatelessSession session, IReadOnlyList<string> fields)
        {
            var item = new NutrientData();

            var NDB_No = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<FoodDescription>(NDB_No);

            var Nutr_No = fields[1].Substring(1, fields[1].Length - 2);
            var nutrientDefinition = session.Get<NutrientDefinition>(Nutr_No);

            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            return nutrientDataKey;
        }
    }
}