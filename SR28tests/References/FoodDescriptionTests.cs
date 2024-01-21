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
using NUnit.Framework.Legacy;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.References
{
    [TestFixture]
    public class FoodDescriptionTests
        : TransactionSetup
    {
        [Test]
        public void FoodDescriptionTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            ClassicAssert.AreEqual("01119", foodDescription.NDB_No);
            ClassicAssert.AreEqual("YOGURT,VANILLA,LOFAT,11 GRAMS PROT PER 8 OZ", foodDescription.Shrt_Desc);
            ClassicAssert.AreEqual("Yogurt, vanilla, low fat, 11 grams protein per 8 ounce", foodDescription.Long_Desc);
        }

        //  Links to the Food Group Description file by the FdGrp_Cd field
        [Test]
        public void FoodGroupTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");

            var foodGroup = foodDescription.FoodGroup;
            ClassicAssert.AreEqual("0100", foodGroup.FdGrp_Cd);
            ClassicAssert.AreEqual("Dairy and Egg Products", foodGroup.FdGrp_Desc);

            ClassicAssert.IsTrue(foodGroup.FoodDescriptionSet.Contains(foodDescription));
        }

        //  Links to the Nutrient Data file by the NDB_No field
        [Test]
        public void NutrientDataTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");

            // var nutrientDataSet = foodDescription.NutrientDataSet;
            // ClassicAssert.AreEqual(91, nutrientDataSet.Count);
            // foreach (var nutrientData in nutrientDataSet)
            //     ClassicAssert.AreEqual(foodDescription, nutrientData.NutrientDataKey.FoodDescription);
        }

        //  Links to the Weight file by the NDB_No field
        [Test]
        public void WeightTest()
        {
            var foodDescription = Session.Load<FoodDescription>("01119");
            
            // var weightSet = foodDescription.WeightSet;
            // ClassicAssert.AreEqual(3, weightSet.Count);
            // foreach (var weight in weightSet) 
            //     ClassicAssert.AreEqual(foodDescription, weight.WeightKey.FoodDescription);
        }

        //  Links to the Footnote file by the NDB_No field
        [Test]
        public void FootnoteTest()
        {
            var foodDescription = Session.Load<FoodDescription>("05315");

            // var footnoteSet = foodDescription.FootnoteSet;
            // ClassicAssert.AreEqual(3, footnoteSet.Count);
            // foreach (var footnote in footnoteSet) 
            //     ClassicAssert.AreEqual(foodDescription, footnote.FoodDescription);
        }

        //  Links to the LanguaL Factor file by the NDB_No field
        [Test]
        public void LanguageTest()
        {
            var foodDescription = Session.Load<FoodDescription>("02002");

            // var languageSet = foodDescription.LanguageSet;
            // ClassicAssert.AreEqual(13, languageSet.Count);
            // foreach (var language in languageSet)
            //     ClassicAssert.IsTrue(language.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}