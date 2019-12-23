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
    public class FootnoteTests
        : NutrishRepository
    {
        public static Footnote CreateFootnote()
        {
            var footnote = new Footnote();
            return footnote;
        }

        [TestMethod]
        public void AddNullNutrientDefinitionTest()
        {
            var footnote = FootnoteTests.CreateFootnote();

            void ClosureContainingCodeToTest() => footnote.AddNutrientDefinition(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientDefinition", exception.Message);
        }

        [TestMethod]
        public void AddNutrientDefinitionTest()
        {
            var footnote = CreateFootnote();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();

            footnote.AddNutrientDefinition(nutrientDefinition);
            Assert.AreSame(nutrientDefinition, footnote.NutrientDefinition);
            Assert.IsTrue(nutrientDefinition.FootnoteSet.Contains(footnote));
        }

        [TestMethod]
        public void AddNullFoodDescriptionTest()
        {
            var footnote = CreateFootnote();

            void ClosureContainingCodeToTest() => footnote.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: foodDescription", exception.Message);
        }

        [TestMethod]
        public void AddFoodDescriptionTest()
        {
            var footnote = CreateFootnote();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();

            footnote.AddFoodDescription(foodDescription);
            Assert.AreSame(foodDescription, footnote.FoodDescription);
            Assert.IsTrue(foodDescription.FootnoteSet.Contains(footnote));
        }
    }
}