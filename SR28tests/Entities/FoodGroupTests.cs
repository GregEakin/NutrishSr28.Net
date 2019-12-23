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
    public class FoodGroupTests
        : NutrishRepository
    {
        public static FoodGroup CreateFoodGroup()
        {
            var foodGroup = new FoodGroup {FdGrp_Cd = "0000"};
            return foodGroup;
        }

        [TestMethod]
        public void AddNullFoodDescriptionTest()
        {
            var foodGroup = CreateFoodGroup();

            void ClosureContainingCodeToTest() => foodGroup.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: foodDescription", exception.Message);
        }

        [TestMethod]
        public void AddFoodDescriptionTest()
        {
            var foodGroup = CreateFoodGroup();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();

            foodGroup.AddFoodDescription(foodDescription);
            Assert.AreSame(foodGroup, foodDescription.FoodGroup);
            Assert.IsTrue(foodGroup.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}