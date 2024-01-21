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
    public class NutrientDataTests
        : TransactionSetup
    {
        public static NutrientData CreateNutrientData(FoodDescription foodDescription,
            NutrientDefinition nutrientDefinition)
        {
            var nutrientDataKey = new NutrientDataKey(foodDescription, nutrientDefinition);
            var nutrientData = new NutrientData {NutrientDataKey = nutrientDataKey, Nutr_Val = 64.0};
            return nutrientData;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<NutrientData>()
                .RowCount();
            ClassicAssert.AreEqual(679045, count);
        }

        [Test]
        public void AddNullDataDerivationTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddDataDerivation(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'dataDerivation')", exception.Message);
        }

        [Test]
        public void AddDataDerivationTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);
            var dataDerivation = DataDerivationTests.CreateDataDerivation();

            nutrientData.AddDataDerivation(dataDerivation);
            ClassicAssert.AreEqual(dataDerivation, nutrientData.DataDerivation);
            ClassicAssert.IsTrue(dataDerivation.NutrientDataSet.Contains(nutrientData));
        }

        [Test]
        public void AddNullDataSourceTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddDataSource(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'dataSource')", exception.Message);
        }

        [Test]
        public void AddDataSourceTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var dataSource = DataSourceTests.CreateDataSource();

            nutrientData.AddDataSource(dataSource);
            ClassicAssert.IsTrue(nutrientData.DataSourceSet.Contains(dataSource));
            ClassicAssert.IsTrue(dataSource.NutrientDataSet.Contains(nutrientData));
        }

        [Test]
        public void SetNullRefFoodDescriptionTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodDescription')", exception.Message);
        }

        [Test]
        public void SetRefFoodDescriptionTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            nutrientData.FoodDescription = foodDescription;
            ClassicAssert.AreSame(foodDescription, nutrientData.FoodDescription);
        }

        [Test]
        public void AddNullSourceCodeTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddSourceCode(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'sourceCode')", exception.Message);
        }

        [Test]
        public void AddSourceCodeTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var sourceCode = SourceCodeTests.CreateSourceCode();

            nutrientData.AddSourceCode(sourceCode);
            ClassicAssert.AreSame(sourceCode, nutrientData.SourceCode);
            ClassicAssert.IsTrue(sourceCode.NutrientDataSet.Contains(nutrientData));
        }

        [Test]
        public void AddNullWeightTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddWeight(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'weight')", exception.Message);
        }

        [Test]
        [Ignore("@@ reason")]
        public void AddWeightTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var weight = WeightTests.CreateWeight(foodDescription);

            nutrientData.AddWeight(weight);
            //ClassicAssert.IsTrue(nutrientData.WeightSet.Contains(weight));
            //ClassicAssert.IsTrue(nutrientData.FoodDescription.WeightSet.Contains(weight));
        }
    }
}