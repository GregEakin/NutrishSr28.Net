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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.DataValidation
{
    [TestClass]
    public class NutrientCountTests
        : NutrishRepository
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context) => BeforeAll(context);

        [ClassCleanup]
        public static void ClassDestructor() => AfterAll();

        private static readonly string[][] Stuff =
        {
            new[] {"255", "Water", "WATER", "g", "8788"},
            new[] {"208", "Energy", "ENERC_KCAL", "kcal", "8789"},
            new[] {"211", "Glucose (dextrose)", "GLUS", "g", "1752"},
            new[] {"204", "Total lipid (fat)", "FAT", "g", "8789"},
        };

        [TestMethod]
        public void CounterTest()
        {
            foreach (var data in Stuff)
            {
                var nutrientDefinition = Session.Load<NutrientDefinition>(data[0]);
                Assert.AreEqual(data[1], nutrientDefinition.NutrDesc);
                Assert.AreEqual(data[2], nutrientDefinition.Tagname);
                Assert.AreEqual(data[3], nutrientDefinition.Units);

                var nutrientDataSet = nutrientDefinition.NutrientDataSet;
                Assert.AreEqual(int.Parse(data[4]), nutrientDataSet.Count);
            }
        }
    }
}