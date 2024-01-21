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

using NUnit.Framework.Legacy;
using SR28lib.Data;
using SR28tests.Utilities;
using System.Linq;
using NUnit.Framework;

namespace SR28tests.DataValidation
{
    [TestFixture]
    public class ElementCountTests
        : TransactionSetup
    {
        [Test]
        public void WaterTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("255");
            ClassicAssert.AreEqual("255", nutrientDefinition.Nutr_No);
            ClassicAssert.AreEqual("Water", nutrientDefinition.NutrDesc);
            ClassicAssert.AreEqual("g", nutrientDefinition.Units);
        }

        [Test]
        public void NutrientDataTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("255");
            var count = Session.QueryOver<NutrientData>()
                .Where(nd => nd.NutrientDataKey.NutrientDefinition == nutrientDefinition)
                .RowCount();

            ClassicAssert.AreEqual(8788, count);
        }

        [Test]
        public void NutrientDataLimitTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("255");
            var list = Session.QueryOver<NutrientData>()
                .Where(nd => nd.NutrientDataKey.NutrientDefinition == nutrientDefinition)
                .OrderBy(nd => nd.NutrientDataKey.FoodDescription.NDB_No).Desc
                .Take(10)
                .List();

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