using System.Collections.Generic;

namespace SR28lib.Data
{
    public class FoodGroup
    {
        public virtual string FdGrp_Cd { get; set; }
        public virtual string FdGrp_Desc { get; set; }
        public virtual IList<FoodDescription> FoodDescriptions { get; set; }

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            foodDescription.FoodGroups.Add(this);
            FoodDescriptions.Add(foodDescription);
        }
    }
}