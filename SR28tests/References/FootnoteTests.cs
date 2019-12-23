﻿// Copyright 2019 Greg Eakin
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
        //  Links to the Food Description file by NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == null)
                .SingleOrDefault();

            Assert.AreEqual("05316", footnote.FoodDescription.NDB_No);
            Assert.IsNull(footnote.NutrientDefinition);
        }

        //  Links to the Nutrient Data file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void NutrientDataTest1()
        {
            var footnote = Session.QueryOver<Footnote>()
                .Where(f => f.FoodDescription.NDB_No == "05316")
                .And(f => f.NutrientDefinition.Nutr_No == null)
                .SingleOrDefault();

            var nutrientDataSet = footnote.FoodDescription.NutrientDataSet;
            Assert.AreEqual(44, nutrientDataSet.Count);
            foreach (var nutrientData in nutrientDataSet)
                Assert.AreEqual(footnote.FoodDescription, nutrientData.NutrientDataKey.FoodDescription);
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

            Assert.AreEqual("204", footnote.NutrientDefinition.Nutr_No);
        }
    }
}