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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace SR28tests.Utilities
{
    [TestClass]
    public abstract class NutrishRepository
    {
        const string Connection =
            "Data Source=(localdb)\\SR28;" +
            "Initial Catalog=Nutrish;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        private static ISessionFactory _factory;
        private static ISession _session;
        private ITransaction _transaction;

        protected ISession Session => _session;

        [ClassInitialize]
        public static void BeforeAll(TestContext context)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = Connection;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.BatchSize = 50;
            });

            var libAssembly = typeof(SR28lib.Data.FoodGroup).Assembly;
            cfg.AddAssembly(libAssembly);

            _factory = cfg.BuildSessionFactory();
            _session = _factory.OpenSession();
        }

        [ClassCleanup]
        public static void AfterAll()
        {
            _session?.Close();
            _factory?.Close();
        }

        [TestInitialize]
        public void BeforeTestExecution()
        {
            _transaction = _session.BeginTransaction();
        }

        [TestCleanup]
        public void AfterTestExecution()
        {
            _transaction.Rollback();
        }
    }
}