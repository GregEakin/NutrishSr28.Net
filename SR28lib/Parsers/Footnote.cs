using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;

namespace SR28lib.Parsers
{
    public static class Footnote
    {
        public static readonly string Filename = "..\\..\\..\\data\\FOOTNOTE.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            Console.WriteLine(line);
            var fields = line.Split('^');
            var item = ParseFootnote(session, fields);
            session.Insert(item);
        }

        private static SR28lib.Data.Footnote ParseFootnote(IStatelessSession session, IReadOnlyList<string> fields)
        {
            var item = new SR28lib.Data.Footnote();
            var foodDescriptionId = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<SR28lib.Data.FoodDescription>(foodDescriptionId);
            item.FoodDescription = foodDescription;

            item.Footnt_No = fields[1].Substring(1, fields[1].Length - 2);

            item.Footnt_Typ = fields[2].Substring(1, fields[2].Length - 2);

            if (fields[3].Length > 2)
            {
                var Nutr_No = fields[3].Substring(1, fields[3].Length - 2);
                var nutrientDefinition = session.Get<SR28lib.Data.NutrientDefinition>(Nutr_No);
                item.NutrientDefinition = nutrientDefinition;
            }

            item.Footnt_Txt = fields[4].Substring(1, fields[4].Length - 2);

            foodDescription.AddFootnote(item);

            return item;
        }
    }
}