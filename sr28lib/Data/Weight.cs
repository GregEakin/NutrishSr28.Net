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
    [DebuggerDisplay("{WeightKey.FoodDescription} {WeightKey.Seq} {Msre_Desc}")]
    public class Weight
    {
        public virtual WeightKey WeightKey { get; set; }
        public virtual double? Amount { get; set; }
        public virtual string Msre_Desc { get; set; }
        public virtual double? Gm_Wgt { get; set; }
        public virtual int? Num_Data_Pts { get; set; }
        public virtual double? Std_Dev { get; set; }

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if (foodDescription == null) throw new ArgumentNullException(nameof(foodDescription));
            throw new NotImplementedException();
        }

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null) throw new ArgumentNullException(nameof(nutrientData));
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public sealed class WeightKey
    {
        public WeightKey()
        {
        }

        public WeightKey(FoodDescription foodDescription, string seq)
        {
            FoodDescription = foodDescription;
            Seq = seq;
        }

        public FoodDescription FoodDescription { get; set; }
        public string Seq { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var that = (WeightKey) obj;
            var same = (FoodDescription?.Equals(that.FoodDescription) ?? that.FoodDescription == null)
                       && (Seq?.Equals(that.Seq) ?? that.Seq == null);
            return same;
        }

        public override int GetHashCode()
        {
            var result = FoodDescription != null ? FoodDescription.GetHashCode() : 0;
            result = 31 * result + (Seq != null ? Seq.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return $"FoodDescriptionKey {FoodDescription}, {Seq}.";
        }
    }
}