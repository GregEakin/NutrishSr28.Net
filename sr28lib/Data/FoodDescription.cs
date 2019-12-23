// Copyright 2019 Greg Eakin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SR28lib.Data
{
    [DebuggerDisplay("{NDB_No} {Shrt_Desc}")]
    public class FoodDescription
    {
        public virtual string NDB_No { get; set; }
        public virtual FoodGroup FoodGroup { get; set; }

        public virtual string Long_Desc { get; set; }
        public virtual string Shrt_Desc { get; set; }
        public virtual string ComName { get; set; }
        public virtual string ManufacName { get; set; }
        public virtual string Survey { get; set; }
        public virtual string Ref_desc { get; set; }
        public virtual int? Refuse { get; set; }
        public virtual string SciName { get; set; }
        public virtual double? N_Factor { get; set; }
        public virtual double? Pro_Factor { get; set; }
        public virtual double? Fat_Factor { get; set; }
        public virtual double? CHO_Factor { get; set; }
        public virtual ISet<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();

        public virtual ISet<Weight> WeightSet { get; set; } = new HashSet<Weight>();

        public virtual ISet<Footnote> FootnoteSet { get; set; } = new HashSet<Footnote>();

        public virtual ISet<Language> LanguageSet { get; set; } = new HashSet<Language>();

        public virtual void AddFoodGroup(FoodGroup foodGroup)
        {
            FoodGroup = foodGroup ?? throw new ArgumentNullException(nameof(foodGroup));
            FoodGroup.FoodDescriptionSet.Add(this);
        }

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            var nutrientDataKey = nutrientData.NutrientDataKey;
            nutrientDataKey.FoodDescription = this;
            NutrientDataSet.Add(nutrientData);
        }

        public virtual void AddWeight(Weight weight)
        {
            if (weight == null)
                throw new ArgumentNullException(nameof(weight));

            if (!weight.WeightKey.FoodDescription.NDB_No.Equals(NDB_No))
                throw new ArgumentException("Weight not related to Food Description.");

            WeightSet.Add(weight);
        }

        public virtual void AddFootnote(Footnote footnote)
        {
            if (footnote == null)
                throw new ArgumentNullException(nameof(footnote));

            footnote.FoodDescription = this;
            FootnoteSet.Add(footnote);
        }

        public virtual void AddLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            language.FoodDescriptionSet.Add(this);
            LanguageSet.Add(language);
        }
    }
}