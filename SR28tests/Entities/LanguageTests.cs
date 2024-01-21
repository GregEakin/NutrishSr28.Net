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
    public class LanguageTests
        : TransactionSetup
    {
        public static Language CreateLanguage()
        {
            var language = new Language {Factor_Code = "00000"};
            return language;
        }

        [Test]
        public void RowCountTest()
        {
            var count = Session
                .QueryOver<Language>()
                .RowCount();
            ClassicAssert.AreEqual(774, count);
        }

        [Test]
        public void AddNullFoodDescription()
        {
            var language = CreateLanguage();

            void ClosureContainingCodeToTest() => language.AddFoodDescription(null);
            var exception = ExpectedException.AssertThrows<ArgumentNullException>(ClosureContainingCodeToTest);
            ClassicAssert.AreEqual("Value cannot be null. (Parameter 'foodDescription')", exception.Message);
        }

        [Test]
        public void AddFoodDescription()
        {
            var language = CreateLanguage();
            var foodDescription = FoodDescriptionTests.CreateFoodDescription();

            language.AddFoodDescription(foodDescription);
            ClassicAssert.IsTrue(language.FoodDescriptionSet.Contains(foodDescription));
            ClassicAssert.IsTrue(foodDescription.LanguageSet.Contains(language));
        }
    }
}