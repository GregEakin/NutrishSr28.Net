using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class DataDerivation
    {
        public virtual string Deriv_Cd { get; set; }
        public virtual string Deriv_Desc { get; set; }
        public virtual IList<NutrientData> NutrientDataSet { get; set; }
    }
}