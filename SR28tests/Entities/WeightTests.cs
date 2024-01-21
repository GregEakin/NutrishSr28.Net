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
    public class WeightTests
        : TransactionSetup
    {
        public static Weight CreateWeight(FoodDescription foodDescription)
        {
            var weightKey = new WeightKey(foodDescription, "00");
            var weight = new Weight {WeightKey = weightKey};
            foodDescription.AddWeight(weight);
            return weight;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<Weight>()
                .RowCount();
            ClassicAssert.AreEqual(15438, count);
        }

        [Test]
        public void AddNullFoodDescription()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            void ClosureContainingCodeToTest() => weight.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodDescription')", exception.Message);
        }

        [Test]
        public void AddFoodDescription()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            ClassicAssert.AreSame(foodDescription, weight.WeightKey.FoodDescription);
            ClassicAssert.IsTrue(foodDescription.WeightSet.Contains(weight));
        }

        [Test]
        public void AddNullNutrientDataTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            void ClosureContainingCodeToTest() => weight.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'nutrientData')", exception.Message);
        }

        [Test]
        public void AddNutrientData()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            ClassicAssert.AreSame(weight.WeightKey.FoodDescription, nutrientData.NutrientDataKey.FoodDescription);
            // ClassicAssert.IsTrue(nutrientData.WeightSet.Contains(weight));
        }

        [Test]
        public void EqualsTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var weight = WeightTests.CreateWeight(foodDescription);

            var foodDescription2 = FoodDescriptionTests.CreateFoodDescription();
            foodDescription2.NDB_No = "123";
            var weight2 = WeightTests.CreateWeight(foodDescription2);

            ClassicAssert.IsFalse(Equals(null, weight));
            ClassicAssert.IsFalse(Equals(weight, null));
            ClassicAssert.IsTrue(Equals(weight, weight));
            ClassicAssert.IsFalse(Equals(weight, weight2));
        }
    }
}