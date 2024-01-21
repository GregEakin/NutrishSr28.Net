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
    public class FoodGroupTests
        : TransactionSetup
    {
        public static FoodGroup CreateFoodGroup()
        {
            var foodGroup = new FoodGroup {FdGrp_Cd = "0000"};
            return foodGroup;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<FoodGroup>()
                .RowCount();
            ClassicAssert.AreEqual(25, count);
        }

        [Test]
        public void AddNullFoodDescriptionTest()
        {
            var foodGroup = CreateFoodGroup();

            void ClosureContainingCodeToTest() => foodGroup.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodDescription')", exception.Message);
        }

        [Test]
        public void AddFoodDescriptionTest()
        {
            var foodGroup = CreateFoodGroup();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();

            foodGroup.AddFoodDescription(foodDescription);
            ClassicAssert.AreSame(foodGroup, foodDescription.FoodGroup);
            ClassicAssert.IsTrue(foodGroup.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}