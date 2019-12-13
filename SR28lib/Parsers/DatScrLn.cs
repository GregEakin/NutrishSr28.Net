using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class DatScrLn
    {
        public static readonly string Filename = "..\\..\\..\\data\\DATSRCLN.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var NDB_No = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<FoodDescription>(NDB_No);

            var Nutr_No = fields[1].Substring(1, fields[1].Length - 2);
            var nutrientDefinition = session.Get<NutrientDefinition>(Nutr_No);

            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = session.Get<NutrientData>(nutrientDataKey);

            var DataSrc_ID = fields[2].Substring(1, fields[2].Length - 2);
            var dataSource = session.Get<DataSource>(DataSrc_ID);

            nutrientData.AddDataSource(dataSource);
        }
    }
}