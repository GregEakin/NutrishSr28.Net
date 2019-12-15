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

namespace SR28lib.Data
{
    public class NutrientData
    {
        public virtual NutrientDataKey NutrientDataKey { get; set; }
        public virtual double Nutr_Val { get; set; }
        public virtual int Num_Data_Pts{ get; set; }
        public virtual double Std_Error{ get; set; }
        public virtual SourceCode SourceCode{ get; set; }

        public virtual DataDerivation DataDerivation{ get; set; }

        public virtual FoodDescription FoodDescription { get; set; }

        public virtual string Add_Nutr_Mark{ get; set; }
        public virtual int Num_Studies{ get; set; }
        public virtual double Min{ get; set; }
        public virtual double Max{ get; set; }
        public virtual int DF{ get; set; }
        public virtual double Low_EB{ get; set; }
        public virtual double Up_EB{ get; set; }
        public virtual string Stat_cmt{ get; set; }
        public virtual string AddMod_Date{ get; set; }
        public virtual string CC{ get; set; }

        public virtual ISet<DataSource> DataSourceSet { get; set; } = new HashSet<DataSource>();

        public virtual void AddSourceCode(SourceCode sourceCode)
        {
            SourceCode = sourceCode ?? throw new ArgumentNullException(nameof(sourceCode));
            //sourceCode.NutrientDataSet.Add(this);
        }

        public virtual void AddDataDerivation(DataDerivation dataDerivation)
        {
            if (dataDerivation == null)
                throw new ArgumentNullException(nameof(dataDerivation));

            //dataDerivation.NutrientDataSet.Add(this);
            // DataDerivation = dataDerivation;
            // dataDerivation.NutrientDataSet.Add(this);
        }

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if (foodDescription == null)
                throw new ArgumentNullException(nameof(foodDescription));
        }

        public virtual void AddDataSource(DataSource dataSource)
        {
            if (dataSource == null)
                throw new ArgumentNullException(nameof(dataSource));

            DataSourceSet.Add(dataSource);
            dataSource.NutrientDataSet.Add(this);
        }
    }

    [Serializable]
    public sealed class NutrientDataKey
    {
        public NutrientDataKey() { }

        public NutrientDataKey(FoodDescription foodDescription, NutrientDefinition nutrientDefinition)
        {
            FoodDescription = foodDescription;
            NutrientDefinition = nutrientDefinition;
        }

        public FoodDescription FoodDescription { get; set; }
        public NutrientDefinition NutrientDefinition { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var that = (NutrientDataKey)obj;
            var same = (FoodDescription?.Equals(that.FoodDescription) ?? that.FoodDescription == null)
                       && (NutrientDefinition?.Equals(that.NutrientDefinition) ?? that.NutrientDefinition == null);
            return same;
        }

        public override int GetHashCode()
        {
            var result = FoodDescription != null ? FoodDescription.GetHashCode() : 0;
            result = 31 * result + (NutrientDefinition != null ? NutrientDefinition.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return $"NutrientDataKey {FoodDescription}, {NutrientDefinition}.";
        }
    }
}