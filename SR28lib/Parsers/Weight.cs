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
using System.IO;

namespace SR28lib.Parsers
{
    public static class Weight
    {
        public const string DataFile = "..\\..\\..\\data\\WEIGHT.txt";
        public const string AddData = "..\\..\\..\\data2\\ADD_WGT.txt";
        public const string ChangeData = "..\\..\\..\\data2\\CHG_WGT.txt";

        public static void ParseFile(IStatelessSession session)
        {
            if (!File.Exists(DataFile)) return;

            var lines = File.ReadLines(DataFile);
            foreach (var line in lines)
                AddLine(session, line);
        }

        public static void ParseUpdates(IStatelessSession session)
        {
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
        }

        private static void AddLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var weight = Item(session, fields);
            session.Insert(weight);
        }

        private static void ChangeLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var weight = Item(session, fields);
            session.Update(weight);
        }

        private static Data.Weight Item(IStatelessSession session, string[] fields)
        {
            var item = new Data.Weight();
            
            // NDB_No A 5* N 5-digit Nutrient Databank number that uniquely identifies a food item.
            // Seq A 2* N Sequence number.
            var foodDescriptionId = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<Data.FoodDescription>(foodDescriptionId);
            item.WeightKey = new Data.WeightKey(foodDescription, fields[1]);

            // Amount N 5.3 N Unit modifier (for example, 1 in “1 cup”).
            item.Amount = double.Parse(fields[2]);

            // Msre_Desc A 84 N Description (for example, cup, diced, and 1-inch pieces).
            item.Msre_Desc = fields[3].Substring(1, fields[3].Length - 2);

            // Gm_Wgt N 7.1 N Gram weight.
            item.Gm_Wgt = double.Parse(fields[4]);

            // Num_Data_Pts N 3 Y Number of data points.
            if (fields[5].Length > 0) item.Num_Data_Pts = int.Parse(fields[5]);

            // Std_Dev N 7.3 Y Standard deviation.
            if (fields[6].Length > 0) item.Std_Dev = double.Parse(fields[6]);
            
            return item;
        }
    }
}