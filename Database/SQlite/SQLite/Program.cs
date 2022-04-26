using System.Data.SQLite;
using Dapper;

namespace SQLite;

public class Program
{
    public static void Main()
    {
        var dbName = "Test.db";
        var persons = GetDataGromDb(dbName);

        persons.ForEach(p => Console.WriteLine($"{p.FirstName} {p.LastName}"));


    }

    private static List<Person> GetDataGromDb(string dbName)
    {
        if (!File.Exists(dbName))
        {
            CreateDb(dbName);
        }
        return GetPersonsFromDb(dbName);
    }

    private static List<Person> GetPersonsFromDb(string dbName)
    {
        using (var connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
        {
            connection.Open();
            var persons = (List<Person>)connection.Query<Person>("SELECT * FROM Persons");
            return persons;
        }
    }

    private static void CreateDb(string dbName)
    {
        using (var db = new SQLiteConnection($"Data Source={dbName};Version=3;"))
        {
            var p = new Person();
            var persons = p.CreatePersons();
            db.Open();
            var command = db.CreateCommand();
            command.CommandText = "CREATE TABLE persons (firstname TEXT, lastname TEXT)";
            command.ExecuteNonQuery();

            foreach (var person in persons)
            {
                command.CommandText = $"INSERT INTO persons (firstname, lastname) VALUES ('{person.FirstName}', '{person.LastName}')";
                command.ExecuteNonQuery();
            }
        }
    }
}