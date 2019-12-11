namespace SR28lib.Data
{
    public class NutrientData
    {
        public virtual NutrientDataKey NutrientDataKey { get; set; }
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