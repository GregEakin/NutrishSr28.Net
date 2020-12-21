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

using NUnit.Framework;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.References
{
    [TestFixture]
    public class FoodGroupTests
        : TransactionSetup
    {
        [Test]
        public void FoodGroupTest()
        {
            var foodGroup = Session.Load<FoodGroup>("0400");
            Assert.AreEqual("0400", foodGroup.FdGrp_Cd);
            Assert.AreEqual("Fats and Oils", foodGroup.FdGrp_Desc);
        }

        //  Links to the Food Description file by FdGrp_Cd
        [Test]
        public void FoodDescriptionTest()
        {
            var foodGroup = Session.Load<FoodGroup>("0400");

            // var foodDescriptionSet = foodGroup.FoodDescriptionSet;
            // Assert.AreEqual(220, foodDescriptionSet.Count);
            //
            // foreach (var foodDescription in foodDescriptionSet) 
            //     Assert.AreEqual(foodGroup, foodDescription.FoodGroup);
        }

}
}