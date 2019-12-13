using System;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class NutrientDefinition
    {
        public virtual string Nutr_No { get; set; }

        public virtual string Units { get; set; }
        public virtual string Tagname { get; set; }
        public virtual string NutrDesc { get; set; }
        public virtual string Num_Dec { get; set; }
        public virtual int SR_Order { get; set; }
        public virtual ICollection<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            // if (nutrientData.NutrientDataKey.NutrientDefinition.)
            NutrientDataSet.Add(nutrientData);
        }

        public virtual ICollection<Footnote> FootnoteSet { get; set; } = new HashSet<Footnote>();

        public virtual void AddFootnote(Footnote footnote)
        {
            if (footnote == null)
                throw new ArgumentNullException(nameof(footnote));

            FootnoteSet.Add(footnote);
            footnote.NutrientDefinition = this;
        }
    }
}