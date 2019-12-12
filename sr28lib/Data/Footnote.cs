namespace SR28lib.Data
{
    public class Footnote
    {
        public virtual int Id { get; set; }
        public virtual FoodDescription FoodDescription { get; set; }
        public virtual string Footnt_No { get; set; }
        public virtual string Footnt_Typ { get; set; }
        public virtual string NutrientDefinition { get; set; }
        public virtual string Footnt_Txt { get; set; }
    }
}