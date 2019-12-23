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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Entities
{
    [TestClass]
    public class NutrientDefinitionTests
        : TransactionSetup
    {
        public static NutrientDefinition CreateNutrientDefinition()
        {
            var nutrientDefinition = new NutrientDefinition {Nutr_No = "000"};
            return nutrientDefinition;
        }

        [TestMethod]
        public void AddNullNutrientDataTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();

            void ClosureContainingCodeToTest() => nutrientDefinition.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientData", exception.Message);
        }

        [TestMethod]
        public void AddNutrientDataTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            nutrientDefinition.AddNutrientData(nutrientData);
            Assert.IsTrue(nutrientDefinition.NutrientDataSet.Contains(nutrientData));
            Assert.AreSame(nutrientDefinition, nutrientData.NutrientDataKey.NutrientDefinition);
        }

        [TestMethod]
        public void AddNullFootnoteTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();

            void ClosureContainingCodeToTest() => nutrientDefinition.AddFootnote(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: footnote", exception.Message);
        }

        [TestMethod]
        public void AddFootnoteTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();
            var footnote = FootnoteTests.CreateFootnote();

            nutrientDefinition.AddFootnote(footnote);
            Assert.AreSame(nutrientDefinition, footnote.NutrientDefinition);
            Assert.IsTrue(nutrientDefinition.FootnoteSet.Contains(footnote));
        }
    }
}