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
using SR28lib.Data;
using SR28tests.Utilities;
using System;
using System.Linq;

namespace SR28tests.DataValidation
{
    [TestClass]
    public class ElementCountTests 
        : NutrishRepository
    {
        [TestMethod]
        public void WaterTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("255");
            Assert.AreEqual("255", nutrientDefinition.Nutr_No);
            Assert.AreEqual("Water", nutrientDefinition.NutrDesc);
            Assert.AreEqual("g", nutrientDefinition.Units);

            var hql = "select count(*) from  NutrientData where Nutr_No = :nutr_no";
            var query = Session.CreateQuery(hql);
            query.SetParameter("nutr_no", "255");
            var count = query.UniqueResult<long>();
            Assert.AreEqual(8788L, count);
        }

        [TestMethod]
        public void WaterLimitTest()
        {
            var hql = "FROM NutrientData "
                      + "WHERE Nutr_No = :nutr_no "
                      + "ORDER BY NDB_No DESC ";
            var query = Session.CreateQuery(hql);
            query.SetParameter("nutr_no", "255");
            query.SetMaxResults(10);
            var list = query.List<NutrientData>();

            CollectionAssert.AreEqual(new[]
            {
                "255",
                "255",
                "255",
                "255",
                "255",
                "255",
                "255",
                "255",
                "255",
                "255",
            }, list.Select(nd => nd.NutrientDataKey.NutrientDefinition.Nutr_No).ToArray());

            CollectionAssert.AreEqual(new[]
            {
                "93600",
                "90560",
                "90480",
                "90240",
                "83110",
                "80200",
                "48052",
                "44260",
                "44259",
                "44258",
            }, list.Select(nd => nd.NutrientDataKey.FoodDescription.NDB_No).ToArray());
        }
    }
}