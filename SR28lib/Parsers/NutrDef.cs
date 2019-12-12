using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class NutrDef
    {
        public static readonly string Filename = "..\\..\\..\\data\\NUTR_DEF.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseDataSource(fields);
            session.Save(item);
        }

        private static NutrientDefinition ParseDataSource(IReadOnlyList<string> fields)
        {
            var item = new NutrientDefinition();

            item.Nutr_No = fields[0].Substring(1, fields[0].Length - 2);
            item.Units = fields[1].Substring(1, fields[1].Length - 2);
            if (fields[2].Length > 2) item.Tagname = fields[2].Substring(1, fields[2].Length - 2);
            item.NutrDesc = fields[3].Substring(1, fields[3].Length - 2);
            item.Num_Dec = fields[4].Substring(1, fields[4].Length - 2);
            item.SR_Order = int.Parse(fields[5].Substring(1, fields[5].Length - 2));

            return item;
        }
    }
}