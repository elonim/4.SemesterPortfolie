using Dapper;
using System.Data.SqlClient;

namespace CopyTableFromDB1ToDB2;

internal class Program
{

    private static void Main(string[] args)
    {
        var conn1 = new SqlConnection("Server=192.168.0.100; Database=CopyDb1; User ID=elonim; Password=XXXXXXXXXXXXXXXXXXX; MultipleActiveResultSets=True;");
        var conn2 = new SqlConnection("Server=192.168.0.100; Database=CopyDb2; User ID=elonim; Password=XXXXXXXXXXXXXXXXXXX; MultipleActiveResultSets=True;");


        //AddPersonsToDatabaseUsingDapper(conn);
        ReadPersonsFromDatabaseUsingDapper(conn1);
        //ReadFromDB1InsertToDB2(conn1, conn2);
    }

    public static void ReadFromDB1InsertToDB2(SqlConnection conn1, SqlConnection conn2)
    {
        var persons = conn1.Query<Person>("SELECT * FROM Persons");

        foreach(var person in persons)
        {
            conn2.Execute("INSERT INTO Persons (ID, FirstName, LastName, Age) VALUES (@Id, @FirstName, @LastName, @Age)", person);
        }
    }

    public static void ReadPersonsFromDatabaseUsingDapper(SqlConnection conn)
    {
        conn.Open();
        var persons = conn.Query<Person>("SELECT * FROM Persons");
        foreach(var person in persons)
        {
            Console.WriteLine($"{person.ID} {person.FirstName} {person.LastName} {person.Age}");
        }
        conn.Close();
    }

    public static void AddPersonsToDatabaseUsingDapper(SqlConnection conn)
    {
        var persons = Create1000RandomPersons();
        conn.Open();
        conn.Execute("INSERT INTO Persons (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)", persons);
        conn.Close();
    }



    public static List<Person> Create1000RandomPersons()
    {
        var numberOfPersons = 1000;
        var persons = new List<Person>();
        var rnd = new Random();
        var firstNames = new[]
        {
            "John", "Jane", "Mary", "Jack", "Mark", "Peter", "Paul", "Bridgit", "Earl", "Robert", "Niomie", "Liam", "Emma", "Olivia", "Sophia", "Ava", "Isabella", "Mia", "Charlotte", "Amelia", "Eve", "Abigail", "Emily", "Harper", "Ella", "Lily", "Chloe", "Sophie", "Zoe", "Victoria", "Mila", "Aria", "Scarlett", "Grace", "Sofia", "Camila", "Aria", "Lillian", "Zoey", "Lilly", "Madison", "Elizabeth", "Chloe", "Eleanor", "Hannah", "Addison", "Natalie", "Luna", "Savannah", "Brooklyn", "Leah", "Zoe", "Victoria", "Stella", "Maya", "Paisley", "Audrey", "Skylar", "Violet", "Claire", "Bella", "Lucy", "Anna", "Caroline", "Samantha", "Mila", "Kennedy", "Genesis", "Aubrey", "Madelyn", "Aurora", "Arianna", "Hailey", "Kaylee", "Allison", "Alexa", "Nevaeh", "Abby", "Sarah", "Anna", "Aaliyah", "Riley", "Camilla", "Savannah", "Piper", "Eleanor", "Sophie", "Scarlett", "Victoria", "Mackenzie", "Madeline", "Evelyn", "Autumn", "Adeline", "Taylor", "Faith", "Alexandra", "Kylie", "Katherine", "Sydney", "Lauren", "Morgan", "Alexis", "London", "Isabelle", "Eleanor", "Madelyn", "Aurora", "Adalyn", "Vivian", "Sophia", "James", "Thomal",
        };
        var lastNames = new[]
        {
            "Doe", "Johnson", "Hanson", "Smith", "Jones", "Brown", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas"
        };

        for(var i = 0; i < numberOfPersons; i++)
        {
            var firstName = firstNames[rnd.Next(0, firstNames.Length)];
            var lastName = lastNames[rnd.Next(0, lastNames.Length)];
            var age = rnd.Next(20, 100);
            persons.Add(new Person { FirstName = firstName, LastName = lastName, Age = age });
        }
        return persons;
    }
}
