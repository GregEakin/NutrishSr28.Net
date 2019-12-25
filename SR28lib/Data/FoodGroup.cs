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
using System.Collections.Generic;
using System.Diagnostics;

namespace SR28lib.Data
{
    [DebuggerDisplay("{FdGrp_Cd} {FdGrp_Desc}")]
    public class FoodGroup
    {
        public virtual string FdGrp_Cd { get; set; }
        public virtual string FdGrp_Desc { get; set; }
        public virtual ISet<FoodDescription> FoodDescriptionSet { get; set; } = new HashSet<FoodDescription>();

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if (foodDescription == null)
                throw new ArgumentNullException(nameof(foodDescription));

            foodDescription.FoodGroup = this;
            FoodDescriptionSet.Add(foodDescription);
        }

        public override int GetHashCode()
        {
            return FdGrp_Cd?.GetHashCode() ?? 0;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || (other is FoodGroup that &&
                                                    Equals(FdGrp_Cd, that.FdGrp_Cd));
        }
    }
}