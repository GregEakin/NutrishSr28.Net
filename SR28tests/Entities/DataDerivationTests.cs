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
    public class DataDerivationTests
        : TransactionSetup
    {
        public static DataDerivation CreateDataDerivation()
        {
            var dataDerivation = new DataDerivation {Deriv_Cd = "0000"};
            return dataDerivation;
        }

        [TestMethod]
        public void AddNullNutrientDataTest()
        {
            var dataDerivation = CreateDataDerivation();

            void ClosureContainingCodeToTest() => dataDerivation.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientData", exception.Message);
        }

        [TestMethod]
        public void AddNutrientDataTest()
        {
            var dataDerivation = CreateDataDerivation();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            dataDerivation.AddNutrientData(nutrientData);
            Assert.AreSame(dataDerivation, nutrientData.DataDerivation);
            Assert.IsTrue(dataDerivation.NutrientDataSet.Contains(nutrientData));
        }
    }
}