using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class DerivCD
    {
        public static readonly string Filename = "..\\..\\..\\data\\DERIV_CD.txt";

        public static void ParseFile(IStatelessSession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(IStatelessSession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseDataDerivation(fields);
            session.Insert(item);
        }

        private static DataDerivation ParseDataDerivation(IReadOnlyList<string> fields)
        {
            var item = new DataDerivation
            {
                Deriv_Cd = fields[0].Substring(1, fields[0].Length - 2),
                Deriv_Desc = fields[1].Substring(1, fields[1].Length - 2)
            };
            return item;
        }
    }
}