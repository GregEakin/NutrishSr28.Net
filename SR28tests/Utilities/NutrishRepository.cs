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

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using SR28lib.Data;
using System;

namespace SR28tests.Utilities
{
    public class NutrishRepository
    {
        private static readonly Lazy<Configuration> configuration_ = new(BuildConfiguration);
        private static readonly Lazy<ISessionFactory> factory_ = new(BuildFactory);
        private static readonly Lazy<ISession> session_ = new(BuildSession);

        private const string Connection =
            "Data Source=(localdb)\\SR28;" +
            "Initial Catalog=Nutrish;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        public static Configuration Configuration => configuration_.Value;

        public static ISessionFactory Factory => factory_.Value;

        public static ISession Session => session_.Value;

        public static Configuration BuildConfiguration()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(x =>
            {
                x.ConnectionString = Connection;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.BatchSize = 50;
                x.LogSqlInConsole = true;
                x.LogFormattedSql = true;
            });

            var libAssembly = typeof(FoodGroup).Assembly;
            configuration.AddAssembly(libAssembly);

            return configuration;
        }

        public static ISessionFactory BuildFactory()
        {
            var factory = Configuration.BuildSessionFactory();
            return factory;
        }
        
        public static ISession BuildSession()
        {
            var session = Factory.OpenSession();

            // Console.WriteLine("BuildSession");
            // foreach (DictionaryEntry property in context.Properties)
            //     Console.WriteLine("Prop {0}, {1}", property.Key, property.Value);
            
            return session;
        }
    }
}