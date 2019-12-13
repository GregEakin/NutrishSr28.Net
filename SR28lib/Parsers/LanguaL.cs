﻿using System;
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