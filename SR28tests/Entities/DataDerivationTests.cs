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
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Entities
{
    [TestFixture]
    public class DataDerivationTests
        : TransactionSetup
    {
        public static DataDerivation CreateDataDerivation()
        {
            var dataDerivation = new DataDerivation {Deriv_Cd = "BUG", Deriv_Desc = "Debugger"};
            return dataDerivation;
        }

        [Test]
        public void CountTest()
        {
            var count = Session
                .QueryOver<DataDerivation>()
                .RowCount();
            ClassicAssert.AreEqual(55, count);
        }

        [Test]
        public void AddNullNutrientDataTest()
        {
            var dataDerivation = CreateDataDerivation();

            void ClosureContainingCodeToTest() => dataDerivation.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'nutrientData')", exception.Message);
        }

        [Test]
        public void AddNutrientDataTest()
        {
            var dataDerivation = CreateDataDerivation();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            dataDerivation.AddNutrientData(nutrientData);
            ClassicAssert.AreSame(dataDerivation, nutrientData.DataDerivation);
            ClassicAssert.IsTrue(dataDerivation.NutrientDataSet.Contains(nutrientData));
        }
    }
}