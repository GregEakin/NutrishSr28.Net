using System;
using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class FoodDescription
    {
        public virtual string NDB_No { get; set; }
        public virtual IList<FoodGroup> FoodGroups { get; set; }

        public void AddFoodGroup(FoodGroup foodGroup)
        {
            if (foodGroup == null)
                throw new ArgumentNullException(nameof(foodGroup));
            // this.foodGroup = foodGroup;
            // this.FoodGroup.FoodDescriptions.add(this); 
        }

        public virtual string Long_Desc { get; set; }
        public virtual string Shrt_Desc { get; set; }
        public virtual string ComName { get; set; }
        public virtual string ManufacName { get; set; }
        public virtual string Survey { get; set; }
        public virtual string Ref_desc { get; set; }
        public virtual string Refuse { get; set; }
        public virtual string SciName { get; set; }
        public virtual string N_Factor { get; set; }
        public virtual string Pro_Factor { get; set; }
        public virtual string Fat_Factor { get; set; }
        public virtual string CHO_Factor { get; set; }
        public virtual IList<NutrientData> NutrientDataSet { get; set; }

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
        }

        public virtual IList<Weight> WeightSet { get; set; }

        public virtual void AddWeight(Weight weight)
        {
        }

        public virtual IList<Footnote> FootnoteSet { get; set; }

        public virtual void AddFootnote(Footnote footnote)
        {
        }
        // many to many: LANGUAL  LanguageSet

        public virtual IList<Language> LanguageSet { get; set; }

        public virtual void AddLanguage(Language language)
        {
        }
    }
}