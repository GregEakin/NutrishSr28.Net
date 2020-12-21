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

namespace SR28tests.DataValidation
{
    [TestFixture]
    public class NutGroupTests
        : TransactionSetup
    {
        [Test]
        public void Test1()
        {
            var foodGroup = Session.Load<FoodGroup>("1200");
            Assert.AreEqual("1200", foodGroup.FdGrp_Cd);
            Assert.AreEqual("Nut and Seed Products", foodGroup.FdGrp_Desc);

            var foodDescriptionSet = foodGroup.FoodDescriptionSet;
            Assert.AreEqual(137, foodDescriptionSet.Count);
        }
    }
}