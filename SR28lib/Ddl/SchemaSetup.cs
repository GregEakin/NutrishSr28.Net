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
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using SR28lib.Parsers;
using System;
using System.Reflection;
using Footnote = SR28lib.Parsers.Footnote;
using Weight = SR28lib.Parsers.Weight;

namespace SR28lib.Ddl
{
    public class SchemaSetup : IDisposable
    {
        private readonly ISessionFactory _factory;
        private readonly IStatelessSession _statelessSession;
        private readonly ISession _session;

        public SchemaSetup(string connection, bool execute)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = connection;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.BatchSize = 50;
            });

            var executingAssembly = Assembly.GetExecutingAssembly();
            cfg.AddAssembly(executingAssembly);

            if (execute)
            {
                var schemaExport = new SchemaExport(cfg);
                var outputFile = schemaExport.SetOutputFile("schema.hibernate5.sql");
                outputFile.Execute(true, true, false);
            }

            _factory = cfg.BuildSessionFactory();
            _statelessSession = _factory.OpenStatelessSession();
            _session = _factory.OpenSession();
        }

        public void Dispose()
        {
            _session?.Dispose();
            _statelessSession?.Dispose();
            _factory?.Dispose();
        }

        public void SetupDates()
        {
            Console.WriteLine("Load FdGroup");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                FdGroup.ParseFile(_statelessSession);
                transaction.Commit();
            }

            Console.WriteLine("Load SrcCd");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                SrcCd.ParseFile(_statelessSession);
                transaction.Commit();
            }

            Console.WriteLine("Load DerivCD");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                DerivCD.ParseFile(_statelessSession);
                transaction.Commit();
            }

            Console.WriteLine("Load LangDesc");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                LangDesc.ParseFile(_statelessSession);
                transaction.Commit();
            }

            Console.WriteLine("Load DataSrc");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                DataSrc.ParseFile(_statelessSession);
                transaction.Commit();
            }

            Console.WriteLine("Load NutrDef");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                NutrDef.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // fd_group
            Console.WriteLine("Load FoodDes");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                FoodDes.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // food_des
            Console.WriteLine("Load Weight");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                Weight.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // langdesc
            // food_des
            Console.WriteLine("Load LanguaL");
            using (var transaction = _session.BeginTransaction())
            {
                LanguaL.ParseFile(_session);
                transaction.Commit();
            }

            // food_des
            // nuter_def
            Console.WriteLine("Load Footnote");
            using (var transaction = _session.BeginTransaction())
            {
                Footnote.ParseFile(_session);
                transaction.Commit();
            }

            // food_des
            // nutr_def
            // src_cd
            // deriv_cd
            // food_des
            Console.WriteLine("Load NutData");
            using (var transaction = _statelessSession.BeginTransaction())
            {
                NutData.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // nut_data
            // data_src
            Console.WriteLine("Load DatScrLn");
            using (var transaction = _session.BeginTransaction())
            {
                DatScrLn.ParseFile(_session);
                transaction.Commit();
            }
        }
    }
}