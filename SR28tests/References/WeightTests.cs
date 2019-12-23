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

namespace SR28tests.References
{
    [TestClass]
    public class WeightTests
        : TransactionSetup
    {
        //  Links to Food Description file by NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var weightKey = new WeightKey(foodDescription, "3 ");
            var weight = Session.Load<Weight>(weightKey);

            Assert.AreSame(foodDescription, weight.WeightKey.FoodDescription);
        }

        //  Links to Nutrient Data file by NDB_No
        [TestMethod]
        public void NutrientDataTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var weightKey = new WeightKey(foodDescription, "3 ");
            var weight = Session.Load<Weight>(weightKey);

            var nutrientDataSet = weight.WeightKey.FoodDescription.NutrientDataSet;
            Assert.AreEqual(91, nutrientDataSet.Count);
        }
    }
}