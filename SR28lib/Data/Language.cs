using System;
using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class Language
    {
        public virtual string Factor_Code { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<FoodDescription> FoodDescriptionSet { get; set; } = new HashSet<FoodDescription>();
        public virtual void AddFoodDescription(FoodDescription foodDescription)
        {
            if (foodDescription == null)
                throw new ArgumentNullException(nameof(foodDescription));

            //FoodDescriptionSet.Add(foodDescription);
            //foodDescription.LanguageSet.Add(this);
        }
    }
}