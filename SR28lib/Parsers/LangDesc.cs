using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class LangDesc
    {
        public static readonly string Filename = "..\\..\\..\\data\\LANGDESC.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseLanguage(fields);
            session.Save(item);
        }

        private static Language ParseLanguage(IReadOnlyList<string> fields)
        {
            var item = new Language
            {
                Factor_Code = fields[0].Substring(1, fields[0].Length - 2),
                Description = fields[1].Substring(1, fields[1].Length - 2)
            };
            return item;
        }
    }
}