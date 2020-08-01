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
using System.Diagnostics;

namespace SR28lib.Data
{
    [DebuggerDisplay("{Id} {Footnt_Txt}")]
    public class Footnote
    {
        public virtual int Id { get; set; }
        public virtual FoodDescription FoodDescription { get; set; }
        public virtual string Footnt_No { get; set; }
        public virtual string Footnt_Typ { get; set; }
        public virtual NutrientDefinition NutrientDefinition { get; set; }
        public virtual string Footnt_Txt { get; set; }

        public virtual void AddNutrientDefinition(NutrientDefinition nutrientDefinition)
        {
            if (nutrientDefinition == null) throw new ArgumentNullException(nameof(nutrientDefinition));
            throw new NotImplementedException();
        }

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if (foodDescription == null) throw new ArgumentNullException(nameof(foodDescription));
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || (other is Footnote that &&
                                                    Equals(Id, that.Id));
        }
    }
}