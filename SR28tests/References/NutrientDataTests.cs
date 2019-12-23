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
    public class NutrientDataTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        //  Links to the Food Description file by Ref_NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = Session.Load<NutrientData>(nutrientDataKey);

            Assert.AreSame(nutrientDataKey, nutrientData.NutrientDataKey);
            Assert.AreSame(foodDescription, nutrientData.NutrientDataKey.FoodDescription);
        }

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

        //  Links to the Footnote file by NDB_No
        [TestMethod]
        public void FootnoteTest1()
        {
            var foodDescription = Session.Load<FoodDescription>("12040");
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            var footnoteSet = nutrientDataKey.FoodDescription.FootnoteSet;
            Assert.AreEqual(2, footnoteSet.Count);
            foreach (var footnote in footnoteSet)
            {
                Assert.AreEqual("12040", footnote.FoodDescription.NDB_No);
                Assert.IsNull(footnote.NutrientDefinition);
            }
        }

        //  Links to the Footnote file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void FootnoteTest2()
        {
            var foodDescription = Session.Load<FoodDescription>("03073");
            var nutrientDefinition = Session.Load<NutrientDefinition>("320");
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);

            String hql =
                "FROM Footnote WHERE FoodDescription.NDB_No = :ndb_no and NutrientDefinition.Nutr_No = :nutr_no";
            var query = Session.CreateQuery(hql);
            query.SetParameter("ndb_no", nutrientDataKey.FoodDescription.NDB_No);
            query.SetParameter("nutr_no", nutrientDataKey.NutrientDefinition.Nutr_No);
            Footnote footnote = query.UniqueResult<Footnote>();

            Assert.AreEqual("03073", footnote.FoodDescription.NDB_No);
            Assert.AreEqual("320", footnote.NutrientDefinition.Nutr_No);
        }

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