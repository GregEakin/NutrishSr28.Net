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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Entities
{
    [TestClass]
    public class DataSourceTests
        : TransactionSetup
    {
        public static DataSource CreateDataSource()
        {
            var dataSource = new DataSource {DataSrc_ID = "000000"};
            return dataSource;
        }

        [TestMethod]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<DataSource>()
                .RowCount();
            Assert.AreEqual(682, count);
        }

        [TestMethod]
        public void AddNullNutrientDataTest()
        {
            var dataSource = CreateDataSource();

            void ClosureContainingCodeToTest() => dataSource.AddNutrientData(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            Assert.AreEqual("Value cannot be null.\r\nParameter name: nutrientData", exception.Message);
        }

        [TestMethod]
        public void AddNutrientDataTest()
        {
            var dataSource = CreateDataSource();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();
            var nutrientDefinition = NutrientDefinitionTests.CreateNutrientDefinition();
            var nutrientData = NutrientDataTests.CreateNutrientData(foodDescription, nutrientDefinition);

            dataSource.AddNutrientData(nutrientData);
            Assert.IsTrue(nutrientData.DataSourceSet.Contains(dataSource));
            Assert.IsTrue(dataSource.NutrientDataSet.Contains(nutrientData));
        }
    }
}