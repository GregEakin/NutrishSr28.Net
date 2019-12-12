using System.Collections.Generic;
using System.IO;
using NHibernate;
using SR28lib.Data;

namespace SR28lib.Parsers
{
    public static class DataSrc
    {
        public static readonly string Filename = "..\\..\\..\\data\\DATA_SRC.txt";

        public static void ParseFile(ISession session)
        {
            var lines = File.ReadLines(Filename);
            foreach (var line in lines)
                ParseLine(session, line);
        }

        private static void ParseLine(ISession session, string line)
        {
            var fields = line.Split('^');
            var item = ParseDataSource(fields);
            session.Save(item);
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