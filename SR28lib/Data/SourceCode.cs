﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace SR28lib.Data
{
    public class SourceCode
    {
        public virtual string Src_Cd { get; set; }

        public virtual string SrcCd_Desc { get; set; }
        public virtual ICollection<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            nutrientData.SourceCode = this;
            NutrientDataSet.Add(nutrientData);
        }
    }
}