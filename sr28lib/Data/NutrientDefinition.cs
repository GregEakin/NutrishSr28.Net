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
        public virtual ICollection<NutrientDefinition> NutrientDefinitionSet { get; set; }
        public virtual ICollection<Footnote> FootnoteSet { get; set; }
    }
}