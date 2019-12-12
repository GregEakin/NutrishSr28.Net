using System.Collections.Generic;

namespace SR28lib.Data
{
    public class FoodGroup
    {
        public virtual string FdGrp_Cd { get; set; }
        public virtual string FdGrp_Desc { get; set; }
        public virtual ICollection<FoodDescription> FoodDescriptionSet { get; set; }

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            foodDescription.FoodGroup = this;
            FoodDescriptionSet.Add(foodDescription);
        }
    }
}