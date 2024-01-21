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
    public class NutrientDefinitionTests
        : TransactionSetup
    {
        public static NutrientDefinition CreateNutrientDefinition()
        {
            var nutrientDefinition = new NutrientDefinition {Nutr_No = "000", NutrDesc = "Bits" };
            return nutrientDefinition;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<NutrientDefinition>()
                .RowCount();
            ClassicAssert.AreEqual(150, count);
        }

        [Test]
        public void AddNullNutrientDataTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();

            void ClosureContainingCodeToTest() => nutrientDefinition.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'nutrientData')", exception.Message);
        }

        [Test]
        public void AddNutrientDataTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            nutrientDefinition.AddNutrientData(nutrientData);
            ClassicAssert.IsTrue(nutrientDefinition.NutrientDataSet.Contains(nutrientData));
            ClassicAssert.AreSame(nutrientDefinition, nutrientData.NutrientDataKey.NutrientDefinition);
        }

        [Test]
        public void AddNullFootnoteTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();

            void ClosureContainingCodeToTest() => nutrientDefinition.AddFootnote(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'footnote')", exception.Message);
        }

        [Test]
        public void AddFootnoteTest()
        {
            var nutrientDefinition = CreateNutrientDefinition();
            var footnote = FootnoteTests.CreateFootnote();

            nutrientDefinition.AddFootnote(footnote);
            ClassicAssert.AreSame(nutrientDefinition, footnote.NutrientDefinition);
            ClassicAssert.IsTrue(nutrientDefinition.FootnoteSet.Contains(footnote));
        }
    }
}