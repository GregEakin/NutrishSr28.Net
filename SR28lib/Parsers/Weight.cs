using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;

namespace SR28lib.Parsers
{
    public static class Weight
    {
        public static readonly string Filename = "..\\..\\..\\data\\WEIGHT.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = new SR28lib.Data.Weight();
            // NDB_No A 5* N 5-digit Nutrient Databank number that uniquely identifies a food item.
            // Seq A 2* N Sequence number.
            var foodDescriptionId = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Load<SR28lib.Data.FoodDescription>(foodDescriptionId);
            item.WeightKey = new SR28lib.Data.WeightKey(foodDescription, fields[1]);

            // Amount N 5.3 N Unit modifier (for example, 1 in “1 cup”).
            item.Amount = double.Parse(fields[2]);

            // Msre_Desc A 84 N Description (for example, cup, diced, and 1-inch pieces).
            var description = fields[3].Substring(1, fields[3].Length - 2);
            item.Msre_Desc = description;

            // Gm_Wgt N 7.1 N Gram weight.
            item.Gm_Wgt = double.Parse(fields[4]);

            // Num_Data_Pts N 3 Y Number of data points.
            if (fields[5].Length > 0) item.Num_Data_Pts = int.Parse(fields[5]);

            // Std_Dev N 7.3 Y Standard deviation.
            if (fields[6].Length > 0) item.Std_Dev = double.Parse(fields[6]);

            foodDescription.AddWeight(item);
            session.Save(item);
        }
    }
}