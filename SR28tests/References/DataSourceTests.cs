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

using NUnit.Framework;
using SR28lib.Data;
using SR28tests.Utilities;
using System.Linq;
using NUnit.Framework.Legacy;

namespace SR28tests.References
{
    [TestFixture]
    public class DataSourceTests
        : TransactionSetup
    {
        [Test]
        public void DataSourceTest()
        {
            var dataSource = Session.Load<DataSource>("D642");
            ClassicAssert.AreEqual("D642", dataSource.DataSrc_ID);
            ClassicAssert.AreEqual(" Consumer Reports", dataSource.Authors);
            ClassicAssert.AreEqual("Consumer Reports", dataSource.Journal);
            ClassicAssert.AreEqual("Orange Drink Mixes", dataSource.Title);
            ClassicAssert.AreEqual("1977", dataSource.Year);
        }

        //  Links to Nutrient Data file by NDB No. through the Sources of Data Link file
        [Test]
        public void NutrientDataTest()
        {
            var dataSource = Session.Load<DataSource>("D642");
            var nutrientDataSet = dataSource.NutrientDataSet;

            var enumerable1 = nutrientDataSet.Select(nd => nd.Nutr_Val);
            CollectionAssert.AreEquivalent(new[] {167.0, 10.0}, enumerable1.ToArray());

            var enumerable2 = nutrientDataSet.Select(nd => nd.AddMod_Date);
            CollectionAssert.AreEqual(new[] {"01/2003", "01/2003"}, enumerable2.ToArray());
        }

        //  Links to the Nutrient Definition file by Nutr_No
        [Test]
        public void NutrientDefinitionTest()
        {
            var dataSource = Session.Load<DataSource>("D642");
            var nutrientDataSet = dataSource.NutrientDataSet;

            var enumerable1 = nutrientDataSet.Select(nd => nd.NutrientDataKey.NutrientDefinition.Nutr_No);
            CollectionAssert.AreEquivalent(new[] {"306", "307"}, enumerable1.ToArray());

            var enumerable2 = nutrientDataSet.Select(nd => nd.NutrientDataKey.NutrientDefinition.NutrDesc);
            CollectionAssert.AreEquivalent(new[] {"Potassium, K", "Sodium, Na"}, enumerable2.ToArray());
        }
    }
}