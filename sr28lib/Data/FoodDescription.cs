using System;
using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class FoodDescription
    {
        public virtual string NDB_No { get; set; }
        public virtual FoodGroup FoodGroup { get; set; }
        public virtual void AddFoodGroup(FoodGroup foodGroup)
        {
            if (foodGroup == null)
                throw new ArgumentNullException(nameof(foodGroup));
            FoodGroup = foodGroup;
            FoodGroup.FoodDescriptionSet.Add(this);
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
        public virtual ICollection<NutrientData> NutrientDataSet { get; set; }
        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            var nutrientDataKey = nutrientData.NutrientDataKey;
            nutrientDataKey.FoodDescription = this;
            NutrientDataSet.Add(nutrientData);
        }

        public virtual ICollection<Weight> WeightSet { get; set; }
        public virtual void AddWeight(Weight weight)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (!weight.WeightKey.FoodDescription.NDB_No.Equals(NDB_No))
                throw new ArgumentException("Weight not related to Food Description.");

            WeightSet.Add(weight);
        }

        public virtual ICollection<Footnote> FootnoteSet { get; set; }
        public virtual void AddFootnote(Footnote footnote)
        {
            if (footnote == null)
                throw new ArgumentNullException(nameof(footnote));

            footnote.FoodDescription = this;
            FootnoteSet.Add(footnote);
        }
        // many to many: LANGUAL  LanguageSet

        public virtual ICollection<Language> LanguageSet { get; set; }

        public virtual void AddLanguage(Language language)
        {
        }
    }
}