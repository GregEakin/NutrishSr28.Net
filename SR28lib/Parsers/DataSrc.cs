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

using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class DataSrc
    {
        public static readonly string Filename = "..\\..\\..\\data\\DATA_SRC.txt";

        public static void ParseFile(IStatelessSession session)
        {
            if (!File.Exists(Filename)) return;
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseDataSource(fields);
            session.Insert(item);
        }

        private static DataSource ParseDataSource(IReadOnlyList<string> fields)
        {
            var item = new DataSource();
            item.DataSrc_ID = fields[0].Substring(1, fields[0].Length - 2);
            if (fields[1].Length > 2) item.Authors = fields[1].Substring(1, fields[1].Length - 2);
            item.Title = fields[2].Substring(1, fields[2].Length - 2);
            if (fields[3].Length > 2) item.Year = fields[3].Substring(1, fields[3].Length - 2);
            if (fields[4].Length > 2) item.Journal = fields[4].Substring(1, fields[4].Length - 2);
            if (fields[5].Length > 2) item.Vol_City = fields[5].Substring(1, fields[5].Length - 2);
            if (fields[6].Length > 2) item.Issue_State = fields[6].Substring(1, fields[6].Length - 2);
            if (fields[7].Length > 2) item.Start_Page = fields[7].Substring(1, fields[7].Length - 2);
            if (fields[8].Length > 2) item.End_Page = fields[8].Substring(1, fields[8].Length - 2);
            return item;
        }
    }
}