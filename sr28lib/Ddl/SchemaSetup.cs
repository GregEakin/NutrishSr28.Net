using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SR28lib.Parsers;
using System;
using System.Reflection;

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
            var libAssembly = typeof(SR28lib.Data.FoodGroup).Assembly;
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
            using (var transaction = _session.BeginTransaction())
            {
                FdGroup.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                SrcCd.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                DerivCD.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                LangDesc.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                DataSrc.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                NutrDef.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                FoodDes.ParseFile(_session);
                transaction.Commit();
            }

            // using (var transaction = _session.BeginTransaction())
            // {
            //     LanguaL.ParseFile(_session);
            //     transaction.Commit();
            // }

            using (var transaction = _session.BeginTransaction())
            {
                Weight.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                NutData.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                DatScrLn.ParseFile(_session);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                Footnote.ParseFile(_session);
                transaction.Commit();
            }
        }
    }
}