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
    public class DataDerivationTests
        : TransactionSetup
    {
        [Test]
        public void DataDerivationTest()
        {
            var dataDerivation = Session.Load<DataDerivation>("RC");
            ClassicAssert.AreEqual("RC", dataDerivation.Deriv_Cd);
            ClassicAssert.AreEqual("Recipe; Cookbook", dataDerivation.Deriv_Desc);
        }

        //  Links to the Nutrient Data file by Deriv_Cd
        [Test]
        public void NutrientDataTest()
        {
            var dataDerivation = Session.Load<DataDerivation>("RC");
            var nutrientDataSet = dataDerivation.NutrientDataSet;
            // ClassicAssert.AreEqual(2358, nutrientDataSet.Count);
            // foreach (var nutrientData in nutrientDataSet) 
            //     ClassicAssert.AreEqual(dataDerivation, nutrientData.DataDerivation);
        }
    }
}