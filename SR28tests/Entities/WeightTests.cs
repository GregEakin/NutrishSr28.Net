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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Entities
{
    [TestClass]
    public class WeightTests
        : NutrishRepository
    {
        public static Weight CreateWeight(FoodDescription foodDescription)
        {
            var weightKey = new WeightKey(foodDescription, "00");
            var weight = new Weight {WeightKey = weightKey};
            foodDescription.AddWeight(weight);
            return weight;
        }

        [TestMethod]
        public void AddNullFoodDescription()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            void ClosureContainingCodeToTest() => weight.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: foodDescription", exception.Message);
        }

        [TestMethod]
        public void AddFoodDescription()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            // weight.addFoodDescription(foodDescription);
            Assert.AreSame(foodDescription, weight.WeightKey.FoodDescription);
            Assert.IsTrue(foodDescription.WeightSet.Contains(weight));
        }

        [TestMethod]
        public void AddNullNutrientDataTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            void ClosureContainingCodeToTest() => weight.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientData", exception.Message);
        }

        [TestMethod]
        public void AddNutrientData()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            Assert.AreSame(weight.WeightKey.FoodDescription, nutrientData.NutrientDataKey.FoodDescription);
            Assert.IsTrue(nutrientData.WeightSet.Contains(weight));
        }
    }
}