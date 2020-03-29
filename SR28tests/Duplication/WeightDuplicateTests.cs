// Copyright 2020 Greg Eakin
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
using NHibernate.Criterion;
using SR28lib.Data;
using SR28tests.Utilities;

namespace SR28tests.Duplication
{
    [TestClass]
    public class WeightDuplicateTests
        : TransactionSetup
    {
        [TestMethod]
        public void Test1()
        {
            var ndb_no = "03072";
            // var weights = Session.Load<Weight>(ndb_no);
            var query = QueryOver.Of<Weight>().Where(w => w.WeightKey.FoodDescription.NDB_No == ndb_no);
            var weights = query.GetExecutableQueryOver(Session).List();
            foreach (var weight in weights)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}:  {5}",
                    weight.WeightKey.FoodDescription.NDB_No,
                    weight.WeightKey.Seq,
                    weight.Amount,
                    weight.Msre_Desc,
                    weight.Gm_Wgt,
                    weight.WeightKey.FoodDescription.Long_Desc);
            }
        }
    }
}