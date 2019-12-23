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
    [DebuggerDisplay("{Nutr_No} {NutrDesc}")]
    public class NutrientDefinition
    {
        public virtual string Nutr_No { get; set; }

        public virtual string Units { get; set; }
        public virtual string Tagname { get; set; }
        public virtual string NutrDesc { get; set; }
        public virtual string Num_Dec { get; set; }
        public virtual int? SR_Order { get; set; }

        public virtual ISet<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();
        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            // if (nutrientData.NutrientDataKey.NutrientDefinition.)
            NutrientDataSet.Add(nutrientData);
        }

        public virtual ISet<Footnote> FootnoteSet { get; set; } = new HashSet<Footnote>();
        public virtual void AddFootnote(Footnote footnote)
        {
            if (footnote == null)
                throw new ArgumentNullException(nameof(footnote));

            FootnoteSet.Add(footnote);
            footnote.NutrientDefinition = this;
        }
    }
}