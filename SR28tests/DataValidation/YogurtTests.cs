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

namespace SR28tests.DataValidation
{
    [TestClass]
    public class YogurtTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        [TestMethod]
        public void FoodDescriptionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            Assert.AreEqual("01119", foodDescription.NDB_No);
            Assert.AreEqual("Yogurt, vanilla, low fat, 11 grams protein per 8 ounce", foodDescription.Long_Desc);
            Assert.AreEqual("YOGURT,VANILLA,LOFAT,11 GRAMS PROT PER 8 OZ", foodDescription.Shrt_Desc);
            Assert.AreEqual(3.87, foodDescription.CHO_Factor);
            Assert.AreEqual(4.27, foodDescription.Pro_Factor);
            Assert.AreEqual(6.38, foodDescription.N_Factor);
        }

        [TestMethod]

        public void FoodGroupTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var foodGroup = foodDescription.FoodGroup;

            Assert.AreEqual("0100", foodGroup.FdGrp_Cd);
            Assert.AreEqual("Dairy and Egg Products", foodGroup.FdGrp_Desc);
        }

        [TestMethod]
        public void FoodDescriptionLoopbackTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDataSet = foodDescription.NutrientDataSet;

            Assert.AreEqual(91, nutrientDataSet.Count);
            foreach (var nutrientData in nutrientDataSet)
            {
                var nutrientDataKey = nutrientData.NutrientDataKey;
                Assert.AreSame(foodDescription, nutrientDataKey.FoodDescription);
            }
        }

        [TestMethod]

        public void NutrientDefinitionLoopbackTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataSet = nutrientDefinition.NutrientDataSet;

            Assert.AreEqual(8789, nutrientDataSet.Count);
            foreach (var nutrientData in nutrientDataSet)
            {
                var nutrientDataKey = nutrientData.NutrientDataKey;
                Assert.AreSame(nutrientDefinition, nutrientDataKey.NutrientDefinition);
            }
        }

        [TestMethod]

        public void NutrientDefinitionTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            Assert.AreEqual("204", nutrientDefinition.Nutr_No);
            Assert.AreEqual("Total lipid (fat)", nutrientDefinition.NutrDesc);
            Assert.AreEqual("FAT", nutrientDefinition.Tagname);
            Assert.AreEqual("g", nutrientDefinition.Units);
        }

        [TestMethod]

        public void NutrientDataTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);
            Assert.AreEqual(nutrientDataKey, nutrientData.NutrientDataKey);
            Assert.AreEqual(1.25, nutrientData.Nutr_Val);
            Assert.IsNull(nutrientData.Max);
            Assert.IsNull(nutrientData.Min);
            Assert.IsNull(nutrientData.Low_EB);
            Assert.IsNull(nutrientData.Up_EB);
        }

        [TestMethod]

        public void WeightTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var weightSet = foodDescription.WeightSet;
            Assert.AreEqual(3, weightSet.Count);

            foreach (var weight in weightSet)
            {
                Assert.AreEqual(1.0, weight.Amount);
                Assert.IsNull(weight.Std_Dev);
                switch (weight.WeightKey.Seq)
                {
                    case "1 ":
                        Assert.AreEqual(170.0, weight.Gm_Wgt);
                        Assert.AreEqual("container (6 oz)", weight.Msre_Desc);
                        break;
                    case "2 ":
                        Assert.AreEqual(227.0, weight.Gm_Wgt);
                        Assert.AreEqual("container (8 oz)", weight.Msre_Desc);
                        break;
                    case "3 ":
                        Assert.AreEqual(245.0, weight.Gm_Wgt);
                        Assert.AreEqual("cup (8 fl oz)", weight.Msre_Desc);
                        break;
                }
            }
        }

        [TestMethod]

        public void FoodDescriptionFootnoteTest()
        {
            var foodDescription = Session.Load<FoodDescription>("05315");
            var footnoteSet = foodDescription.FootnoteSet;
            Assert.AreEqual(3, footnoteSet.Count);
            foreach (var footnote in footnoteSet)
            {
                Console.WriteLine("    Footnote: " + footnote.Footnt_Txt);
            }
        }

        [TestMethod]

        public void NutrientDefinitionFootnoteTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var footnoteSet = nutrientDefinition.FootnoteSet;
            Assert.AreEqual(13, footnoteSet.Count);

            var foodDescription = Session.Load<FoodDescription>("04673");
            // Assert.IsTrue(footnoteSet.stream().anyMatch(o -> o.FoodDescription == foodDescription));
            // Assert.IsTrue(footnoteSet.stream().map(Footnote::getFoodDescription).anyMatch(foodDescription::equals));

//        Stream<FoodDescription> foodDescriptionStream = footnoteSet.stream().map(Footnote::getFoodDescription).filter(o -> o.NDB_No() == "04673");
//        FoodDescription f2 = foodDescriptionStream.findFirst().();
//        Assertions.assertSame(foodDescription, f2);

            foreach (var footnote in footnoteSet)
            {
                Console.WriteLine("    Footnote: " + footnote.Footnt_Txt + " " +
                                   footnote.FoodDescription.NDB_No);

                // if (footnote.FoodDescription.NDB_No.equals("04673"))
                //     Assert.AreEqual("contains 2.841 g omega-3 fatty acids", footnote.Footnt_Txt);
            }
        }

        [TestMethod]

        public void SourceCodeTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var sourceCode = nutrientData.SourceCode;
            Assert.AreEqual("1", sourceCode.Src_Cd);
            Assert.AreEqual("Analytical or derived from analytical", sourceCode.SrcCd_Desc);
        }

        [TestMethod]

        public void DataDerivationTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("313");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            var dataDerivation = nutrientData.DataDerivation;
            Assert.AreEqual("A", dataDerivation.Deriv_Cd);
            Assert.AreEqual("Analytical data", dataDerivation.Deriv_Desc);
        }
    }
}