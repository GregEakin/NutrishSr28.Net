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
using SR28lib.Data;

namespace SR28tests.Utilities
{
    [TestClass]
    public class NutrishRepository
    {
        private const string Connection =
            "Data Source=(localdb)\\SR28;" +
            "Initial Catalog=Nutrish;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        public static ISessionFactory Factory { get; private set; }
        public static ISession Session { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = Connection;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.BatchSize = 50;
                x.LogSqlInConsole = true;
                x.LogFormattedSql = true;
            });

            var libAssembly = typeof(FoodGroup).Assembly;
            cfg.AddAssembly(libAssembly);

            Factory = cfg.BuildSessionFactory();
            Session = Factory.OpenSession();

            // Console.WriteLine("AssemblyInitialize");
            // foreach (DictionaryEntry property in context.Properties)
            //     Console.WriteLine("Prop {0}, {1}", property.Key, property.Value);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Console.WriteLine("AssemblyCleanup");
        }

        [TestMethod]
        public void InitTest()
        {
        }
    }
}