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
    public class FoodDescriptionTests
        : TransactionSetup
    {
        public static FoodDescription CreateFoodDescription()
        {
            var foodDescription = new FoodDescription {NDB_No = "000000", Shrt_Desc = "CPU Size"};
            return foodDescription;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<FoodDescription>()
                .RowCount();
            ClassicAssert.AreEqual(8789, count);
        }

        [Test]
        public void AddNullFoodGroupTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddFoodGroup(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodGroup')", exception.Message);
        }

        [Test]
        public void AddFoodGroupTest()
        {
            var foodDescription = CreateFoodDescription();
            var foodGroup = FoodGroupTests.CreateFoodGroup();

            foodDescription.AddFoodGroup(foodGroup);
            ClassicAssert.AreSame(foodGroup, foodDescription.FoodGroup);
            ClassicAssert.IsTrue(foodGroup.FoodDescriptionSet.Contains(foodDescription));
        }

        [Test]
        public void AddNullWeightTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddWeight(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'weight')", exception.Message);
        }

        [Test]
        public void AddWeightTest()
        {
            var foodDescription = CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            foodDescription.AddWeight(weight);
            ClassicAssert.IsTrue(foodDescription.WeightSet.Contains(weight));
            ClassicAssert.AreSame(foodDescription, weight.WeightKey.FoodDescription);
        }

        [Test]
        public void AddNullFootnoteTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddFootnote(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'footnote')", exception.Message);
        }

        [Test]
        public void AddFootnoteTest()
        {
            var foodDescription = CreateFoodDescription();
            var footnote = FootnoteTests.CreateFootnote();

            foodDescription.AddFootnote(footnote);
            ClassicAssert.IsTrue(foodDescription.FootnoteSet.Contains(footnote));
            ClassicAssert.AreSame(foodDescription, footnote.FoodDescription);
        }

        [Test]
        public void AddNullLanguageTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddLanguage(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'language')", exception.Message);
        }

        [Test]
        public void AddLanguageTest()
        {
            var foodDescription = CreateFoodDescription();
            var language = LanguageTests.CreateLanguage();

            foodDescription.AddLanguage(language);
            ClassicAssert.IsTrue(foodDescription.LanguageSet.Contains(language));
            ClassicAssert.IsTrue(language.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}