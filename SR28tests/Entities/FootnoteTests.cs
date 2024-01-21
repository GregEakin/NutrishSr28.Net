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
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Entities
{
    [TestFixture]
    public class FootnoteTests
        : TransactionSetup
    {
        public static Footnote CreateFootnote()
        {
            var footnote = new Footnote();
            return footnote;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<Footnote>()
                .RowCount();
            ClassicAssert.AreEqual(552, count);
        }

        [Test]
        public void AddNullNutrientDefinitionTest()
        {
            var footnote = FootnoteTests.CreateFootnote();

            void ClosureContainingCodeToTest() => footnote.AddNutrientDefinition(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'nutrientDefinition')", exception.Message);
        }

        [Test]
        [Ignore("@@ reason")]
        public void AddNutrientDefinitionTest()
        {
            var footnote = CreateFootnote();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();

            footnote.AddNutrientDefinition(nutrientDefinition);
            ClassicAssert.AreSame(nutrientDefinition, footnote.NutrientDefinition);
            ClassicAssert.IsTrue(nutrientDefinition.FootnoteSet.Contains(footnote));
        }

        [Test]
        public void AddNullFoodDescriptionTest()
        {
            var footnote = CreateFootnote();

            void ClosureContainingCodeToTest() => footnote.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodDescription')", exception.Message);
        }

        [Test]
        [Ignore("@@ reason")]
        public void AddFoodDescriptionTest()
        {
            var footnote = CreateFootnote();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();

            footnote.AddFoodDescription(foodDescription);
            ClassicAssert.AreSame(foodDescription, footnote.FoodDescription);
            ClassicAssert.IsTrue(foodDescription.FootnoteSet.Contains(footnote));
        }
    }
}