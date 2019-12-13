using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class FoodDes
    {
        public static readonly string Filename = "..\\..\\..\\data\\FOOD_DES.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines) 
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseFoodDescription(session, fields);
            session.Insert(item);
        }

        private static FoodDescription ParseFoodDescription(IStatelessSession session, IReadOnlyList<string> fields)
        {
            var item = new FoodDescription();
            item.NDB_No = fields[0].Substring(1, fields[0].Length - 2);

            var foodGroupId = fields[1].Substring(1, fields[1].Length - 2);
            var foodGroup = session.Get<FoodGroup>(foodGroupId);
            item.AddFoodGroup(foodGroup);

            item.Long_Desc = fields[2].Substring(1, fields[2].Length - 2);
            item.Shrt_Desc = fields[3].Substring(1, fields[3].Length - 2);
            if (fields[4].Length > 2) item.ComName = fields[4].Substring(1, fields[4].Length - 2);
            if (fields[5].Length > 2) item.ManufacName = fields[5].Substring(1, fields[5].Length - 2);
            if (fields[6].Length > 2) item.Survey = fields[6].Substring(1, fields[6].Length - 2);
            if (fields[7].Length > 2) item.Ref_desc = fields[7].Substring(1, fields[7].Length - 2);
            if (fields[8].Length > 0) item.Refuse = int.Parse(fields[8]);
            if (fields[9].Length > 2) item.SciName = fields[9].Substring(1, fields[9].Length - 2);
            if (fields[10].Length > 0) item.N_Factor = double.Parse(fields[10]);
            if (fields[11].Length > 0) item.Pro_Factor = double.Parse(fields[11]);
            if (fields[12].Length > 0) item.Fat_Factor = double.Parse(fields[12]);
            if (fields[13].Length > 0) item.CHO_Factor = double.Parse(fields[13]);
            return item;
        }
    }
}