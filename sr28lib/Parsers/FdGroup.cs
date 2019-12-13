using System;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class FdGroup
    {
        public static readonly string Filename = "..\\..\\..\\data\\FD_GROUP.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
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