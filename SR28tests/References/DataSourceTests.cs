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

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.References
{
    [TestClass]
    public class DataSourceTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        //  Links to Nutrient Data file by NDB No. through the Sources of Data Link file
        [TestMethod]
        public void NutrientDataTest()
        {
            var dataSource = Session.Load<DataSource>("D642");

            var nutrientDataSet = dataSource.NutrientDataSet;
            Assert.AreEqual(2, nutrientDataSet.Count);
        }

        //  Links to the Nutrient Definition file by Nutr_No
        [TestMethod]
        public void NutrientDefinitionTest()
        {
            //DataSource dataSource = session.load(DataSource.class, "D642");

            var hql =
                "select nd from DataSource ds join ds.NutrientDataSet nds join nds.NutrientDataKey.NutrientDefinition nd where ds.DataSrc_ID = :id";
            var query = Session.CreateQuery(hql);
            query.SetParameter("id", "D642");
            var list = query.List();
            Assert.AreEqual(2, list.Count);
            //CollectionAssert.AreEquivalent(new[] {"306", "307"},
            //list.stream().map(NutrientDefinition::getNutr_No).sorted().toArray());
        }
    }
}