using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLite;

public class Person
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";


    public List<Person> CreatePersons()
    {
        var persons = new List<Person>();
        persons.Add(new Person { FirstName = "John", LastName = "Doe" });
        persons.Add(new Person { FirstName = "Jane", LastName = "Doe" });
        persons.Add(new Person { FirstName = "Joe", LastName = "Doe" });
        return persons;
    }
}

