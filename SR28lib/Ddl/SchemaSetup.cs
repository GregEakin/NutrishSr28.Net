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
                // x.Driver<MicrosoftDataSqlClientDriver>();
                // x.Dialect<MsSql2012Dialect>();
                x.Driver<NpgsqlDriver>();
                x.Dialect<PostgreSQL83Dialect>();
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
            _statelessSession?.Dispose();
            _session?.Dispose();
            _factory?.Dispose();
        }

        public void SetupDates()
        {
            using (var transaction = _session.BeginTransaction())
            {
                FdGroup.ParseFile(_statelessSession);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                SrcCd.ParseFile(_statelessSession);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                DerivCD.ParseFile(_statelessSession);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                LangDesc.ParseFile(_statelessSession);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                DataSrc.ParseFile(_statelessSession);
                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                NutrDef.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // fd_group
            using (var transaction = _session.BeginTransaction())
            {
                FoodDes.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // food_des
            using (var transaction = _session.BeginTransaction())
            {
                Weight.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // langdesc
            // food_des
            using (var transaction = _session.BeginTransaction())
            {
                LanguaL.ParseFile(_session);
                transaction.Commit();
            }

            // food_des
            // nuter_def
            using (var transaction = _session.BeginTransaction())
            {
                Footnote.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // food_des
            // nutr_def
            // src_cd
            // deriv_cd
            // food_des
            using (var transaction = _session.BeginTransaction())
            {
                NutData.ParseFile(_statelessSession);
                transaction.Commit();
            }

            // nut_data
            // data_src
            using (var transaction = _session.BeginTransaction())
            {
                DatScrLn.ParseFile(_session);
                transaction.Commit();
            }
        }
    }
}
