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
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.DataValidation
{
    [TestFixture]
    public class NutrientCountTests
        : TransactionSetup
    {
        // [Test]
        // [DataRow("255", "Water", "WATER", "g", "8788")]
        // [DataRow("208", "Energy", "ENERC_KCAL", "kcal", "8789")]
        // [DataRow("211", "Glucose (dextrose)", "GLUS", "g", "1752")]
        // [DataRow("204", "Total lipid (fat)", "FAT", "g", "8789")]
        public void CounterTest(string id, string nutrDesc, string tagname, string units, string count)
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>(id);
            Assert.AreEqual(nutrDesc, nutrientDefinition.NutrDesc);
            Assert.AreEqual(tagname, nutrientDefinition.Tagname);
            Assert.AreEqual(units, nutrientDefinition.Units);

            // var nutrientDataSet = nutrientDefinition.NutrientDataSet;
            // Assert.AreEqual(int.Parse(count), nutrientDataSet.Count);
        }
    }
}