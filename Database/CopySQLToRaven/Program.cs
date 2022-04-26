using Dapper;
using Raven.Client.Documents;
using System.Data.SqlClient;

namespace CopySQLToRaven;

internal class Program
{
    private static IDocumentStore CreateStore()
    {
        IDocumentStore store = new DocumentStore()
        {
            // Define the cluster node URLs (required)
            Urls = new[] { "http://localhost:8080", 
                    /*some additional nodes of this cluster*/ },

            // Set conventions as necessary (optional)
            Conventions =
                {
                    MaxNumberOfRequestsPerSession = 10,
                    UseOptimisticConcurrency = true
                },
            // Define a default database (optional)
            Database = "CopyDbFraSql"

            // Define a client certificate (optional)
            //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),

            // Initialize the Document Store
        }.Initialize();
        // ***  Opret indeks

        return store;
    }

    private static void Main(string[] args)
    {
        var conn = new SqlConnection("Server=elonim.dyndns.dk; Database=CopyDb1; User ID=elonim; Password=XXXXXXXXXXXXXXXXXXX; MultipleActiveResultSets=True;");
        var persons = GetPersonsUsingDapper(conn);

        AddPersonsToRavendb(persons);


    }

    private static void AddPersonsToRavendb(List<Person> persons)
    {
        using var store = CreateStore();
        using var session = store.OpenSession();
        {
            foreach (var person in persons)
            {
                session.Store(person);
            }
            session.SaveChanges();
        }
    }






    private static List<Person> GetPersonsUsingDapper(SqlConnection conn)
    {
        var persons = conn.Query<Person>("SELECT * FROM Persons").ToList();
        return persons;
    }
}