using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class Language
    {
        public virtual string Factor_Code { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<FoodDescription> FoodDescriptionSet { get; set; }
    }
}