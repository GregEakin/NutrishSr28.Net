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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.References
{
    [TestClass]
    public class NutrientDataTests
        : TransactionSetup
    {
        [TestMethod]
        public void NutrientDataTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01115");
            var nutrientDefinition = Session.Load<NutrientDefinition>("203");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            Assert.AreEqual(nutrientDataKey, nutrientData.NutrientDataKey);
            Assert.AreEqual(12.93, nutrientData.Nutr_Val);
            Assert.AreEqual("11/1976", nutrientData.AddMod_Date);
        }
        
        //  Links to the Food Description file by Ref_NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01171");
            var nutrientDefinition = Session.Load<NutrientDefinition>("221");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var refFoodDescription = nutrientData.FoodDescription;
            Assert.AreEqual("01123", refFoodDescription.NDB_No);
        }

        // TODO: Do we need this link?
        //  Links to the Weight file by NDB_No
        //    [TestMethod]
        //    public void weightTest() {
        //        FoodDescription foodDescription = Session.Load<FoodDescription.class, "01119");
        //        NutrientDefinition nutrientDefinition = Session.Load<NutrientDefinition.class, "204");
        //        NutrientDataKey nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
        //        NutrientData nutrientData = Session.Load<NutrientData.class, nutrientDataKey);
        //
        //        Set<Weight> weightSet = nutrientData.getNutrientDataKey().getFoodDescription().getWeightSet();
        //        assertEquals(3, weightSet.size());
        //    }

        // TODO: Do we want this link?
        //  Links to the Footnote file by NDB_No
        // [TestMethod]
        // public void FootnoteTest1()
        // {
        //     var foodDescription = Session.Load<FoodDescription>("12040");
        //     var nutrientDefinition = Session.Load<NutrientDefinition>("204");
        //     var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
        //     var nutrientData = Session.Load<NutrientData>(nutrientDataKey);
        //
        //     var footnoteSet = nutrientData.FootnoteSet;
        //     Assert.AreEqual(2, footnoteSet.Count);
        //     foreach (var footnote in footnoteSet) 
        //         Assert.AreEqual(foodDescription, footnote.FoodDescription);
        // }

        //  Links to the Footnote file by NDB_No and when applicable, Nutr_No
        //[TestMethod]
        //public void FootnoteTest2()
        //{
        //    var foodDescription = Session.Load<FoodDescription>("17267");
        //    var nutrientDefinition = Session.Load<NutrientDefinition>("208");
        //    var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
        //    var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

        //    var footnoteSet = nutrientData.FootnoteSet;
        //    Assert.AreEqual(1, footnoteSet.Count);
        //    foreach (var footnote in footnoteSet)
        //    {
        //        Assert.AreEqual(foodDescription, footnote.FoodDescription);
        //        Assert.AreEqual(nutrientDefinition, footnote.NutrientDefinition);
        //    }
        //}

        //  Links to the Sources of Data Link file by NDB_No and Nutr_No
        [TestMethod]
        public void DataSourceTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("313");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var dataSourceSet = nutrientData.DataSourceSet;
            Assert.AreEqual(1, dataSourceSet.Count);
        }

        //  Links to the Nutrient Definition file by Nutr_No
        [TestMethod]
        public void NutrientDefinitionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            Assert.AreEqual(nutrientDataKey, nutrientData.NutrientDataKey);
            Assert.AreSame(nutrientDefinition, nutrientData.NutrientDataKey.NutrientDefinition);
        }

        //  Links to the Source Code file by Src_Cd
        [TestMethod]
        public void SourceCodeTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("317");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var sourceCode = nutrientData.SourceCode;
            Assert.AreEqual("4", sourceCode.Src_Cd);
            Assert.AreEqual("Calculated or imputed", sourceCode.SrcCd_Desc);
        }

        //  Links to the Data Derivation Code Description file by Deriv_Cd
        [TestMethod]
        public void DataDerivationTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("317");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var dataDerivation = nutrientData.DataDerivation;
            Assert.AreEqual("BFNN", dataDerivation.Deriv_Cd);
            Assert.AreEqual(
                "Based on another form of the food or similar food; Concentration adjustment; Non-fat solids; Retention factors not used",
                dataDerivation.Deriv_Desc);
        }
    }
}