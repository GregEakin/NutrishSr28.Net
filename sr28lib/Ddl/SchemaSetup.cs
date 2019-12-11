using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SR28lib.Data;
using SR28lib.Parsers;

namespace SR28lib.Ddl
{
    public class SchemaSetup : IDisposable
    {
        private readonly ISessionFactory _factory;
        private readonly ISession _session;

        public SchemaSetup(string connection, bool execute)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = connection;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.BatchSize = 1000;
            });

            var executingAssembly = Assembly.GetExecutingAssembly();
            cfg.AddAssembly(executingAssembly);
            var libAssembly = typeof(FoodGroup).Assembly;
            //cfg.AddAssembly(libAssembly);

            if (execute)
                new SchemaExport(cfg).SetOutputFile("schema.hibernate5.sql").Execute(true, true, false);

            _factory = cfg.BuildSessionFactory();
            _session = _factory.OpenSession();
        }

        public void Dispose()
        {
            _session?.Dispose();
            _factory?.Dispose();
        }

        public void SetupDates()
        {
            FdGroup.ParseFile(_session);

            // foreach (var project in projects)
            // {
            //     Console.WriteLine("SetupDates: {0}", project);
            //     SprintInformation.PopulateIteration(new Uri(_server), project);
            // }
            //
            // foreach (var sprintInfo in SprintInformation.SList)
            //     using (var tx = _session.BeginTransaction())
            //     {
            //         var count = sprintInfo.Value[0].Split(' ')[0];
            //         var parsed = int.TryParse(count, out var days);
            //         if (!parsed) continue;
            //         var end = sprintInfo.Key + TimeSpan.FromDays(days - 1);
            //
            //         var dbDate = _session.QueryOver<SprintDate>().Where(sd => sd.Start == sprintInfo.Key).SingleOrDefault();
            //         var sprintDate = dbDate ?? new SprintDate
            //         {
            //             Start = sprintInfo.Key,
            //             End = end,
            //             Days = days,
            //         };
            //
            //         _session.SaveOrUpdate(sprintDate);
            //
            //         foreach (var yy in sprintInfo.Value)
            //         {
            //             if (yy.Contains(" days.")) continue;
            //             var pw = yy.Replace(@"\Iteration\", @"\");
            //
            //             var dbSprint = _session.QueryOver<Sprint>().Where(ss => ss.Name == pw).SingleOrDefault();
            //             var sprint = dbSprint ?? new Sprint
            //             {
            //                 Name = pw,
            //                 Calendar = sprintDate,
            //             };
            //
            //             if (dbSprint == null) sprintDate.AddSprint(sprint);
            //             _session.SaveOrUpdate(sprint);
            //         }
            //
            //         tx.Commit();
            //     }
        }

    }
}