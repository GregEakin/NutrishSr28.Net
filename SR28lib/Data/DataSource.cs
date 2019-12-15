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
        public virtual ISet<NutrientData> NutrientDataSet { get; set; } = new HashSet<NutrientData>();

        public virtual void AddNutrientData(NutrientData nutrientData)
        {
            if (nutrientData == null)
                throw new ArgumentNullException(nameof(nutrientData));
            nutrientData.DataSourceSet.Add(this);
            NutrientDataSet.Add(nutrientData);
        }
    }
}