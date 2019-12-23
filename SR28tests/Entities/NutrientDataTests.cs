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

        [TestMethod]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<NutrientData>()
                .RowCount();
            Assert.AreEqual(679045, count);
        }

        [TestMethod]
        public void AddNullDataDerivationTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddDataDerivation(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: dataDerivation", exception.Message);
        }

        [TestMethod]
        public void AddDataDerivationTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);
            var dataDerivation = DataDerivationTests.CreateDataDerivation();

            nutrientData.AddDataDerivation(dataDerivation);
            Assert.AreEqual(dataDerivation, nutrientData.DataDerivation);
            Assert.IsTrue(dataDerivation.NutrientDataSet.Contains(nutrientData));
        }

        [TestMethod]
        public void AddNullDataSourceTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddDataSource(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: dataSource", exception.Message);
        }

        [TestMethod]
        public void AddDataSourceTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var dataSource = DataSourceTests.CreateDataSource();

            nutrientData.AddDataSource(dataSource);
            Assert.IsTrue(nutrientData.DataSourceSet.Contains(dataSource));
            Assert.IsTrue(dataSource.NutrientDataSet.Contains(nutrientData));
        }

        [TestMethod]
        public void SetNullRefFoodDescriptionTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: foodDescription", exception.Message);
        }

        [TestMethod]
        public void SetRefFoodDescriptionTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            nutrientData.FoodDescription = foodDescription;
            Assert.AreSame(foodDescription, nutrientData.FoodDescription);
        }

        [TestMethod]
        public void AddNullSourceCodeTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddSourceCode(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: sourceCode", exception.Message);
        }

        [TestMethod]
        public void AddSourceCodeTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var sourceCode = SourceCodeTests.CreateSourceCode();

            nutrientData.AddSourceCode(sourceCode);
            Assert.AreSame(sourceCode, nutrientData.SourceCode);
            Assert.IsTrue(sourceCode.NutrientDataSet.Contains(nutrientData));
        }

        [TestMethod]
        public void AddNullWeightTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);

            void ClosureContainingCodeToTest() => nutrientData.AddWeight(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: weight", exception.Message);
        }

        [TestMethod]
        [Ignore]
        public void AddWeightTest()
        {
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = CreateNutrientData(foodDescription, nutrientDefinition);
            var weight = WeightTests.CreateWeight(foodDescription);

            nutrientData.AddWeight(weight);
            //Assert.IsTrue(nutrientData.WeightSet.Contains(weight));
            //Assert.IsTrue(nutrientData.FoodDescription.WeightSet.Contains(weight));
        }
    }
}