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
    public class NutrientDefinitionTests
        : TransactionSetup
    {
        [Test]
        public void NutrientDefinitionTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            ClassicAssert.AreEqual("204", nutrientDefinition.Nutr_No);
            ClassicAssert.AreEqual("FAT", nutrientDefinition.Tagname);
            ClassicAssert.AreEqual("Total lipid (fat)", nutrientDefinition.NutrDesc);
        }

        //  Links to the Nutrient Data file by Nutr_No
        [Test]
        public void NutrientDataTest()
        {
            var nutrientDefinition = Session.Load<NutrientDefinition>("204");
            var nutrientDataSet = nutrientDefinition.NutrientDataSet;
            // ClassicAssert.AreEqual(8789, nutrientDataSet.Count);
            // foreach (var nutrientData in nutrientDataSet)
            //     ClassicAssert.AreEqual(nutrientDefinition, nutrientData.NutrientDataKey.NutrientDefinition);
        }
    }
}