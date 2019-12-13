﻿using System;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class FoodGroup
    {
        public virtual string FdGrp_Cd { get; set; }
        public virtual string FdGrp_Desc { get; set; }
        public virtual ISet<FoodDescription> FoodDescriptionSet { get; set; } = new HashSet<FoodDescription>();

        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if(foodDescription == null)
                throw new ArgumentException(nameof(foodDescription));

            foodDescription.FoodGroup = this;
            FoodDescriptionSet.Add(foodDescription);
        }
    }
}