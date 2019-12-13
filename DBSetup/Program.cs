namespace DBSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connection =
                "Data Source=(localdb)\\SR28;" +
                "Initial Catalog=Nutrish;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False";

            var execute = true;

            using (var schema = new SR28lib.Ddl.SchemaSetup(connection, false))
            {
                if (execute)
                {
                    schema.SetupDates();
                }
            }
        }
    }
}
