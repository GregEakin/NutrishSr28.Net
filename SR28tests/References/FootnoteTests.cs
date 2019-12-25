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

namespace SR28tests.References
{
    [TestClass]
    public class FootnoteTests
        : TransactionSetup
    {
        [TestMethod]
        public void FootnoteTest()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == null)
                .SingleOrDefault();
            // Assert.AreEqual(1217, footnote.Id);
            Assert.AreEqual("05316", footnote.FoodDescription.NDB_No);
            Assert.AreEqual("01", footnote.Footnt_No);
            Assert.AreEqual("D", footnote.Footnt_Typ);
            Assert.IsNull(footnote.NutrientDefinition);
            Assert.AreEqual("Skinless pieces, charbroiled 12 minutes to  165 degrees F", footnote.Footnt_Txt);
        }

        //  Links to the Food Description file by NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == null)
                .SingleOrDefault();

            var foodDescription = footnote.FoodDescription;
            Assert.AreEqual("05316", foodDescription.NDB_No);
            Assert.IsTrue(foodDescription.FootnoteSet.Contains(footnote));
        }

        //  Links to the Nutrient Data file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void NutrientDataTest1()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == null)
                .SingleOrDefault();

            // var nutrientDataSet = footnote.FoodDescription.NutrientDataSet;
            // Assert.AreEqual(44, nutrientDataSet.Count);
            // foreach (var nutrientData in nutrientDataSet)
            //     Assert.AreEqual(footnote.FoodDescription, nutrientData.NutrientDataKey.FoodDescription);
        }

        //  Links to the Nutrient Data file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void NutrientDataTest2()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == "204")
                .SingleOrDefault();

            // var nutrientData = footnote.NutrientData;
            var nutrientData = Session.QueryOver<NutrientData>()
                .Where(nd => nd.NutrientDataKey.FoodDescription == footnote.FoodDescription)
                .And(nd => nd.NutrientDataKey.NutrientDefinition == footnote.NutrientDefinition)
                .SingleOrDefault();

            Assert.AreEqual(footnote.FoodDescription, nutrientData.NutrientDataKey.FoodDescription);
            Assert.AreEqual(footnote.NutrientDefinition, nutrientData.NutrientDataKey.NutrientDefinition);
        }

        //  Links to the Nutrient Definition file by Nutr_No, when applicable
        [TestMethod]
        public void NutrientDefinitionTest()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == "204")
                .SingleOrDefault();

            var nutrientDefinition = footnote.NutrientDefinition;
            Assert.AreEqual("204", nutrientDefinition.Nutr_No);
            Assert.IsTrue(nutrientDefinition.FootnoteSet.Contains(footnote));
        }
    }
}