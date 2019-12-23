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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SR28tests.DataValidation
{
    [TestClass]
    public class FoodSearchTests
        : TransactionSetup
    {
        [TestMethod]
        public void SortedQueryTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01001");
            Assert.AreEqual("01001", foodDescription.NDB_No);
            Assert.AreEqual("BUTTER,WITH SALT", foodDescription.Shrt_Desc);
            Assert.AreEqual("Butter, salted", foodDescription.Long_Desc);

            var expected1 = new[] {"pat (1\" sq, 1/3\" high)", "tbsp", "cup", "stick"};
            var enumerable1 = foodDescription.WeightSet.Select(w => w.Msre_Desc);
            CollectionAssert.AreEquivalent(expected1, enumerable1.ToArray());

            var expected2 = new[] {5.0, 14.2, 227, 113};
            var enumerable2 = foodDescription.WeightSet.Select(w => w.Gm_Wgt);
            CollectionAssert.AreEquivalent(expected2, enumerable2.ToArray());

            Assert.AreEqual(115, foodDescription.NutrientDataSet.Count);
            // foreach (var nutrientData in foodDescription.NutrientDataSet)
            // {
            //     var nutrientDefinition = nutrientData.NutrientDataKey.NutrientDefinition;
            //     Console.WriteLine(
            //         $"   NutData: {nutrientDefinition.SR_Order}, {nutrientDefinition.NutrDesc} = {nutrientData.Nutr_Val} {nutrientDefinition.Units}");
            // }
        }
    }
}