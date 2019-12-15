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

using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class LanguaL
    {
        public static readonly string Filename = "..\\..\\..\\data\\LANGUAL.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');

            var NDB_no = fields[0].Substring(1, fields[0].Length - 2);
            var foodDescription = session.Get<FoodDescription>(NDB_no);

            var factor_code = fields[1].Substring(1, fields[1].Length - 2);
            var language = session.Get<Language>(factor_code);

            language.AddFoodDescription(foodDescription);

            session.Save(language);
        }
    }
}