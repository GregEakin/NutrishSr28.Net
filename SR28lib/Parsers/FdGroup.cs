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
using SR28lib.Data;
using System.IO;

namespace SR28lib.Parsers
{
    public static class FdGroup
    {
        public const string DataFile = "..\\..\\..\\data\\FD_GROUP.txt";

        public static void ParseFile(IStatelessSession session)
        {
            if (!File.Exists(DataFile)) return;

            var lines = File.ReadLines(DataFile);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var item = new FoodGroup
            {
                FdGrp_Cd = fields[0].Substring(1, fields[0].Length - 2),
                FdGrp_Desc = fields[1].Substring(1, fields[1].Length - 2)
            };
            
            session.Insert(item);
        }
    }
}