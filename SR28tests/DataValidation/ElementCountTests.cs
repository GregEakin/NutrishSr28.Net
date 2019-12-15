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
using System;

namespace SR28tests.DataValidation
{
    [TestClass]
    public class ElementCountTests
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

        // [ClassCleanup]
        public static void AfterAll()
        {
            //if (_session != null) _session.Close();
        }

        [TestMethod]
        public void WaterTest()
        {
            var nutrientDefinition = _session.Load<NutrientDefinition>("255");
            Assert.AreEqual("255", nutrientDefinition.Nutr_No);
            Assert.AreEqual("Water", nutrientDefinition.NutrDesc);
            Assert.AreEqual("g", nutrientDefinition.Units);

            var hql = "select count(*) from  NutrientData where nutr_No = :nutr_no";
            var query = _session.CreateQuery(hql);
            query.SetParameter("nutr_no", "255");
            var count = query.UniqueResult<long>();
            Assert.AreEqual(8788L, count);
        }

        [TestMethod]
        public void WaterLimitTest()
        {
            var hql = "FROM NutrientData "
                      + "WHERE nutr_No = :nutr_no "
                      + "ORDER BY NDB_No DESC ";
            var query = _session.CreateQuery(hql);
            query.SetParameter("nutr_no", "255");
            query.SetMaxResults(10);
            var list = query.List<NutrientData>();
            Assert.AreEqual(10, list.Count);

            foreach (var nutrientData in list)
            {
                Assert.AreEqual("255", nutrientData.NutrientDataKey.NutrientDefinition.Nutr_No);
                Console.WriteLine(nutrientData.NutrientDataKey.FoodDescription.NDB_No);
            }
        }
    }
}