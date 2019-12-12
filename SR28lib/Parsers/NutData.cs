﻿using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class NutData
    {
        public static readonly string Filename = "..\\..\\..\\data\\NUT_DATA.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseDataSource(session, fields);
            session.Save(item);
        }

        private static NutrientData ParseDataSource(ISession session, IReadOnlyList<string> fields)
        {
            var item = new NutrientData();

            var NDB_No = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Load<FoodDescription>(NDB_No);

            var Nutr_No = fields[1].Substring(1, fields[1].Length - 2);
            var nutrientDefinition = session.Load<NutrientDefinition>(Nutr_No);

            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            item.NutrientDataKey = nutrientDataKey;

            item.Nutr_Val = double.Parse(fields[2]);

            item.Num_Data_Pts = int.Parse(fields[3]);

            if (fields[4].Length > 0) item.Std_Error = double.Parse(fields[4]);

            var Src_Cd = fields[5].Substring(1, fields[5].Length - 2);
            var sourceCode = session.Load<SourceCode>(Src_Cd);
            item.AddSourceCode(sourceCode);

            if (fields[6].Length > 2)
            {
                var Deriv_Cd = fields[6].Substring(1, fields[6].Length - 2);
                var dataDerivation = session.Load<DataDerivation>(Deriv_Cd);
                item.AddDataDerivation(dataDerivation);
            }

            if (fields[7].Length > 2)
            {
                var Ref_NDB_No = fields[7].Substring(1, fields[7].Length - 2);
                var refFoodDescription = session.Load<FoodDescription>(Ref_NDB_No);
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

            foodDescription.AddNutrientData(item);
            nutrientDefinition.AddNutrientData(item);

            return item;
        }
    }
}