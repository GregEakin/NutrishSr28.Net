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
    public class FoodDescriptionTests
        : TransactionSetup
    {
        //  Links to the Food Group Description file by the FdGrp_Cd field
        [TestMethod]
        public void FoodGroupTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");

            var foodGroup = foodDescription.FoodGroup;
            Assert.AreEqual("0100", foodGroup.FdGrp_Cd);
            Assert.AreEqual("Dairy and Egg Products", foodGroup.FdGrp_Desc);
        }

        //  Links to the Nutrient Data file by the NDB_No field
        [TestMethod]
        public void NutrientDataTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");

            var nutrientDataSet = foodDescription.NutrientDataSet;
            Assert.AreEqual(91, nutrientDataSet.Count);
        }

//  Links to the Weight file by the NDB_No field
        [TestMethod]
        public void WeightTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");

            var weightSet = foodDescription.WeightSet;
            Assert.AreEqual(3, weightSet.Count);
        }

        //  Links to the Footnote file by the NDB_No field
        [TestMethod]
        public void FootnoteTest()
        {
            var foodDescription = Session.Load<FoodDescription>("05315");

            var footnoteSet = foodDescription.FootnoteSet;
            Assert.AreEqual(3, footnoteSet.Count);
        }

        //  Links to the LanguaL Factor file by the NDB_No field
        [TestMethod]
        public void LanguageTest()
        {
            var foodDescription = Session.Load<FoodDescription>("02002");

            var languageSet = foodDescription.LanguageSet;
            Assert.AreEqual(13, languageSet.Count);
        }
    }
}