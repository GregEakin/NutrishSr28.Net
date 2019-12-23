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
    public class LanguageTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        //  Links to the Food Description file by the NDB_No field
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var language = Session.Load<Language>("A0143");
            var foodDescriptionSet = language.FoodDescriptionSet;
            Assert.AreEqual(232, foodDescriptionSet.Count);
        }

        //  Links to LanguaL Factors Description file by the Factor_Code field
        [TestMethod]
        public void LanguageSetTest()
        {
            var foodDescription = Session.Load<FoodDescription>("02014");
            var languageSet = foodDescription.LanguageSet;
            Assert.AreEqual(13, languageSet.Count);
        }
    }
}