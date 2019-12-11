using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connection =
                "Data Source=(localdb)\\ProjectsV13;" +
                "Initial Catalog=SprintData;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            var execute = false;

            using (var schema = new SR28lib.Ddl.SchemaSetup(connection, execute))
            {
                if (execute)
                {
                    schema.SetupDates();
                }
            }
        }
    }
}
