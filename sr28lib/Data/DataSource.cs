using System;
using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class DataSource
    {
        public virtual string DataSrc_ID { get; set; }

        public virtual string Authors { get; set; }
        public virtual string Title { get; set; }
        public virtual string Year { get; set; }
        public virtual string Journal { get; set; }
        public virtual string Vol_City { get; set; }
        public virtual string Issue_State { get; set; }
        public virtual string Start_Page { get; set; }
        public virtual string End_Page { get; set; }
        public virtual IList<NutrientData> NutrientDataSet { get; set; }

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));
            nutrientData.DataSourceSet.Add(this);
            NutrientDataSet.Add(nutrientData);
        }
    }
}