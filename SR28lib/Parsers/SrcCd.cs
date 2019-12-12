using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class SrcCd
    {
        public static readonly string Filename = "..\\..\\..\\data\\SRC_CD.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseSourceCode(fields);
            session.Save(item);
        }

        private static SourceCode ParseSourceCode(IReadOnlyList<string> fields)
        {
            var item = new SourceCode
            {
                Src_Cd = fields[0].Substring(1, fields[0].Length - 2),
                SrcCd_Desc = fields[1].Substring(1, fields[1].Length - 2)
            };
            return item;
        }
    }
}