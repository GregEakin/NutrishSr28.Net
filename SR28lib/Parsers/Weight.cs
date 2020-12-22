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
        public static readonly string Filename = "data/WEIGHT.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
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

            //foodDescription.AddWeight(item);
            session.Insert(item);
        }
    }
}
