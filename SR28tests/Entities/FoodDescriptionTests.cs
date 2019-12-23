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
    public class FoodDescriptionTests
        : TransactionSetup
    {
        public static FoodDescription CreateFoodDescription()
        {
            var foodDescription = new FoodDescription {NDB_No = "000000", Shrt_Desc = "CPU Size"};
            return foodDescription;
        }

        [TestMethod]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<FoodDescription>()
                .RowCount();
            Assert.AreEqual(8789, count);
        }

        [TestMethod]
        public void AddNullFoodGroupTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddFoodGroup(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: foodGroup", exception.Message);
        }

        [TestMethod]
        public void AddFoodGroupTest()
        {
            var foodDescription = CreateFoodDescription();
            var foodGroup = FoodGroupTests.CreateFoodGroup();

            foodDescription.AddFoodGroup(foodGroup);
            Assert.AreSame(foodGroup, foodDescription.FoodGroup);
            Assert.IsTrue(foodGroup.FoodDescriptionSet.Contains(foodDescription));
        }

        [TestMethod]
        public void AddNullWeightTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddWeight(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: weight", exception.Message);
        }

        [TestMethod]
        public void AddWeightTest()
        {
            var foodDescription = CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            foodDescription.AddWeight(weight);
            Assert.IsTrue(foodDescription.WeightSet.Contains(weight));
            Assert.AreSame(foodDescription, weight.WeightKey.FoodDescription);
        }

        [TestMethod]
        public void AddNullFootnoteTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddFootnote(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: footnote", exception.Message);
        }

        [TestMethod]
        public void AddFootnoteTest()
        {
            var foodDescription = CreateFoodDescription();
            var footnote = FootnoteTests.CreateFootnote();

            foodDescription.AddFootnote(footnote);
            Assert.IsTrue(foodDescription.FootnoteSet.Contains(footnote));
            Assert.AreSame(foodDescription, footnote.FoodDescription);
        }

        [TestMethod]
        public void AddNullLanguageTest()
        {
            var foodDescription = CreateFoodDescription();

            void ClosureContainingCodeToTest() => foodDescription.AddLanguage(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: language", exception.Message);
        }

        [TestMethod]
        public void AddLanguageTest()
        {
            var foodDescription = CreateFoodDescription();
            var language = LanguageTests.CreateLanguage();

            foodDescription.AddLanguage(language);
            Assert.IsTrue(foodDescription.LanguageSet.Contains(language));
            Assert.IsTrue(language.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}