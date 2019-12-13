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
        private static ISessionFactory _factory;
        private static ISession _session;

        const string Connection =
            "Data Source=(localdb)\\SR28;" +
            "Initial Catalog=Nutrish;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

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