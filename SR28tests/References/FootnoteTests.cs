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

namespace SR28tests.References
{
    [TestClass]
    public class FootnoteTests
        : TransactionSetup
    {
        //  Links to the Food Description file by NDB_No
        [TestMethod]
        public void FoodDescriptionTest()
        {
            var hql = "FROM Footnote WHERE FoodDescription.NDB_No = :ndb_no and NutrientDefinition.Nutr_No is null";
            var query = Session.CreateQuery(hql);
            query.SetParameter("ndb_no", "05316");
            var footnote = query.UniqueResult<Footnote>();
            
            var foodDescription = footnote.FoodDescription;
            Assert.AreEqual("05316", foodDescription.NDB_No);
        }

        //  Links to the Nutrient Data file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void NutrientDataTest1()
        {
            var hql = "FROM Footnote WHERE FoodDescription.NDB_No = :ndb_no and NutrientDefinition.Nutr_No is null";
            var query = Session.CreateQuery(hql);
            query.SetParameter("ndb_no", "05316");
            var footnote = query.UniqueResult<Footnote>();

            var foodDescription = footnote.FoodDescription;
            var nutrientDataSet = foodDescription.NutrientDataSet;
            Assert.AreEqual(44, nutrientDataSet.Count);
            foreach (var nutrientData in nutrientDataSet)
                Assert.AreEqual("05316", nutrientData.NutrientDataKey.FoodDescription.NDB_No);
        }

        //  Links to the Nutrient Data file by NDB_No and when applicable, Nutr_No
        [TestMethod]
        public void NutrientDataTest2()
        {
            // String hql = "FROM Footnote WHERE foodDescription.NDB_No = :ndb_no and nutrientDefinition.nutr_No = :nutr_no";
            // Query<Footnote> query = session.createQuery(hql, Footnote.class);
            // query.setParameter("ndb_no", "05316");
            // query.setParameter("nutr_no", "204");
            // Footnote footnote = query.getSingleResult();

            var hql =
                "FROM NutrientData WHERE NutrientDataKey.FoodDescription.NDB_No = :ndb_no and NutrientDataKey.NutrientDefinition.Nutr_No = :nutr_no";
            var query = Session.CreateQuery(hql);
            query.SetParameter("ndb_no", "05316"); // set from footnote.FoodDescription.NDB_No
            query.SetParameter("nutr_no", "204"); // set from footnote.NutrientDefinition.Nutr_No
            var nutrientData = query.UniqueResult<NutrientData>();
            Assert.AreEqual("05316", nutrientData.NutrientDataKey.FoodDescription.NDB_No);
            Assert.AreEqual("204", nutrientData.NutrientDataKey.NutrientDefinition.Nutr_No);
        }

        //  Links to the Nutrient Definition file by Nutr_No, when applicable
        [TestMethod]
        public void NutrientDefinitionTest()
        {
            var hql =
                "FROM Footnote WHERE FoodDescription.NDB_No = :ndb_no and NutrientDefinition.Nutr_No = :nutr_no";
            var query = Session.CreateQuery(hql);
            query.SetParameter("ndb_no", "05316");
            query.SetParameter("nutr_no", "204");
            var footnote = query.UniqueResult<Footnote>();
            
            var nutrientDefinition = footnote.NutrientDefinition;
            Assert.AreEqual("204", nutrientDefinition.Nutr_No);
        }
    }
}