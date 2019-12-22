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

namespace SR28tests.Entities
{
    [TestClass]
    public class SourceCodeTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        public static SourceCode CreateSourceCode()
        {
            var sourceCode = new SourceCode {Src_Cd = "00"};
            return sourceCode;
        }

        [TestMethod]
        public void AddNullNutrientDataTest()
        {
            SourceCode sourceCode = CreateSourceCode();

            void ClosureContainingCodeToTest() => sourceCode.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientData", exception.Message);
        }

    [TestMethod]
        public void AddNutrientDataTest()
        {
            SourceCode sourceCode = CreateSourceCode();
            FoodDescription foodDescription = FoodDescriptionTests.CreateFoodDescription();
            NutrientDefinition nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            NutrientData nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            sourceCode.AddNutrientData(nutrientData);
            Assert.AreSame(sourceCode, nutrientData.SourceCode);
            Assert.IsTrue(sourceCode.NutrientDataSet.Contains(nutrientData));
        }
    }
}