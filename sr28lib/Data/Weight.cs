using System;

namespace SR28lib.Data
{
    public class Weight
    {
        public virtual WeightKey WeightKey { get; set; }
        public virtual double Amount { get; set; }
        public virtual string Msre_Desc { get; set; }
        public virtual double Gm_Wgt { get; set; }
        public virtual int Num_Data_Pts { get; set; }
        public virtual int Std_Dev { get; set; }
    }

    [Serializable]
    public class WeightKey
    {
        public virtual FoodDescription FoodDescription { get; set; }
        public virtual string Seq { get; set; }

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
            var result = (FoodDescription != null ? FoodDescription.GetHashCode() : 0);
            result = 31 * result + (Seq != null ? Seq.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return $"FoodDescriptionKey {FoodDescription}, {Seq}.";
        }
    }
}