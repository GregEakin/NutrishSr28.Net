﻿// Copyright 2019 Greg Eakin
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

using SR28lib.Ddl;

// var connection = "Data Source=(localdb)\\SR28;Initial Catalog=Nutrish;Integrated Security=True";

const string connection =
    "Server=homer.lab.eakin.wtf;" +
    "User ID=postgres;" +
    "Password=sqlserver;" +
    "Database=sr28;" +
    "Search Path=nutrish;" +
    "Pooling=true;" +
    "Enlist=true;" +
    "options=--search_path=nutrish;";

using var schema = new SchemaSetup(connection, true);
schema.SetupDates();
