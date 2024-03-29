﻿// Copyright 2019 Greg Eakin
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
    public class LanguageTests
        : TransactionSetup
    {
        [Test]
        public void LanguageTest()
        {
            var language = Session.Load<Language>("A0143");
            ClassicAssert.AreEqual("A0143", language.Factor_Code);
            ClassicAssert.AreEqual("FRUIT OR FRUIT PRODUCT (US CFR)", language.Description);
        }

        //  Links to the Food Description file by the NDB_No field
        [Test]
        public void FoodDescriptionTest()
        {
            var language = Session.Load<Language>("A0143");
            var foodDescriptionSet = language.FoodDescriptionSet;
            // ClassicAssert.AreEqual(232, foodDescriptionSet.Count);
            // foreach (var foodDescription in foodDescriptionSet)
            //     ClassicAssert.IsTrue(foodDescription.LanguageSet.Contains(language));
        }

        //  Links to LanguaL Factors Description file by the Factor_Code field
        [Test]
        public void LanguageSetTest()
        {
            var foodDescription = Session.Load<FoodDescription>("02014");
            var languageSet = foodDescription.LanguageSet;
            // ClassicAssert.AreEqual(13, languageSet.Count);
            // foreach (var language in languageSet) 
            //     ClassicAssert.IsTrue(language.FoodDescriptionSet.Contains(foodDescription));
        }
    }
}