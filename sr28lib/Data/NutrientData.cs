﻿using System.Collections;
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

        public virtual IList<DataSource> DataSourceSet { get; set; }
        public virtual void AddDataSource(DataSource dataSource)
        {
        }
    }

    public class NutrientDataKey
    {
        public virtual FoodDescription FoodDescription { get; set; }
        public virtual NutrientDefinition NutrientDefinition { get; set; }

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
            var result = (FoodDescription != null ? FoodDescription.GetHashCode() : 0);
            result = 31 * result + (NutrientDefinition != null ? NutrientDefinition.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return $"NutrientDataKey {FoodDescription}, {NutrientDefinition}.";
        }
    }
}