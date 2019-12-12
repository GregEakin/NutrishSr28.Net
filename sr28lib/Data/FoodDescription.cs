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
            FoodGroup = foodGroup ?? throw new ArgumentNullException(nameof(foodGroup));
            FoodGroup.FoodDescriptionSet.Add(this);
        }

        public virtual string Long_Desc { get; set; }
        public virtual string Shrt_Desc { get; set; }
        public virtual string ComName { get; set; }
        public virtual string ManufacName { get; set; }
        public virtual string Survey { get; set; }
        public virtual string Ref_desc { get; set; }
        public virtual int Refuse { get; set; }
        public virtual string SciName { get; set; }
        public virtual double N_Factor { get; set; }
        public virtual double Pro_Factor { get; set; }
        public virtual double Fat_Factor { get; set; }
        public virtual double CHO_Factor { get; set; }
        public virtual ICollection<NutrientData> NutrientDataSet { get; set; } = new List<NutrientData>();
        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            var nutrientDataKey = nutrientData.NutrientDataKey;
            nutrientDataKey.FoodDescription = this;
            NutrientDataSet.Add(nutrientData);
        }

        public virtual ICollection<Weight> WeightSet { get; set; } = new List<Weight>();
        public virtual void AddWeight(Weight weight)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (!weight.WeightKey.FoodDescription.NDB_No.Equals(NDB_No))
                throw new ArgumentException("Weight not related to Food Description.");

            WeightSet.Add(weight);
        }

        public virtual ICollection<Footnote> FootnoteSet { get; set; } = new List<Footnote>();
        public virtual void AddFootnote(Footnote footnote)
        {
            if (footnote == null)
                throw new ArgumentNullException(nameof(footnote));

            footnote.FoodDescription = this;
            FootnoteSet.Add(footnote);
        }

        public virtual ICollection<Language> LanguageSet { get; set; } = new List<Language>();
        public virtual void AddLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            language.FoodDescriptionSet.Add(this);
            LanguageSet.Add(language);
        }
    }
}