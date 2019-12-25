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
    [DebuggerDisplay("{Src_Cd} {SrcCd_Desc}")]
    public class SourceCode
    {
        public virtual string Src_Cd { get; set; }

        public virtual string SrcCd_Desc { get; set; }
        public virtual ISet<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));

            nutrientData.SourceCode = this;
            NutrientDataSet.Add(nutrientData);
        }

        public override int GetHashCode()
        {
            return Src_Cd?.GetHashCode() ?? 0;
        }

        public override bool Equals(object other)
        {
            return ReferenceEquals(this, other) || (other is SourceCode that && Equals(Src_Cd, that.Src_Cd));
        }
    }
}