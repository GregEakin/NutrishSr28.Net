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
    public class FattyAcidTests 
        : NutrishRepository
    {
        [TestMethod]
        public void ButyricTest()
        {

            var nutrientDefinition = Session.Load<NutrientDefinition>( "607");
            Assert.AreEqual("4:0", nutrientDefinition.NutrDesc);
            Assert.AreEqual("F4D0", nutrientDefinition.Tagname);
            Assert.AreEqual("g", nutrientDefinition.Units);

        }

        [TestMethod]
        public void CaproicTest()
        {

            var nutrientDefinition = Session.Load<NutrientDefinition>("608");
            Assert.AreEqual("6:0", nutrientDefinition.NutrDesc);
            Assert.AreEqual("F6D0", nutrientDefinition.Tagname);
            Assert.AreEqual("g", nutrientDefinition.Units);

        }

        [TestMethod]
        public void MyristoleicTest()
        {

            var nutrientDefinition = Session.Load<NutrientDefinition>("625");
            Assert.AreEqual("14:1", nutrientDefinition.NutrDesc);
            Assert.AreEqual("F14D1", nutrientDefinition.Tagname);
            Assert.AreEqual("g", nutrientDefinition.Units);

        }
    }
}