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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.DataValidation
{
    [TestClass]
    public class FoodSearchTests
        : NutrishRepository
    {
        [TestMethod]
        public void SortedQueryTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01001");
            Console.WriteLine("Basic Report: " + foodDescription.NDB_No
                                               + ", " + foodDescription.Long_Desc);

            Console.WriteLine("   Weight: Value per 100 g");
            var weightSet = foodDescription.WeightSet;
            foreach (var weight in weightSet)
            {
                Console.WriteLine(
                    "   Weight: " + weight.Msre_Desc + ", " + weight.Amount + " x " + weight.Gm_Wgt + " g");
            }

            var hql = "select nds "
                      + "from FoodDescription fd join fd.NutrientDataSet nds "
                      + "where fd.NDB_No = :id "
                      + "order by nds.NutrientDataKey.NutrientDefinition.SR_Order";
            var query = Session.CreateQuery(hql);
            query.SetParameter("id", "01001");
            var list = query.List<NutrientData>();

            //        Set<NutrientData> nutrientDataSet = foodDescription.getNutrientDataSet();
            //        Comparator<NutrientData> nutrientDataComparator = Comparator.comparingInt(o -> o.getNutrientDataKey().getNutrientDefinition().getSR_Order());
            //        List<NutrientData> list = nutrientDataSet.stream().sorted(nutrientDataComparator).collect(Collectors.toList());

            Assert.AreEqual(115, list.Count);
            foreach (var nutrientData in list)
            {
                var nutrientDataKey = nutrientData.NutrientDataKey;
                var nutrientDefinition = nutrientDataKey.NutrientDefinition;

                Console.WriteLine("   NutData: " + nutrientDefinition.SR_Order
                                                 + ", " + nutrientDefinition.NutrDesc
                                                 + " = " + nutrientData.Nutr_Val
                                                 + " " + nutrientDefinition.Units);
            }
        }
    }
}