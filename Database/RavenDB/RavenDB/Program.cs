using Raven.Client.Documents;
using RavenDB.DTO;
using RavenDB.Index;

namespace RavenDB;

class Program
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
            Database = "KundeOrdreTestDB",

            // Define a client certificate (optional)
            //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),

            // Initialize the Document Store
        }.Initialize();
        // ***  Opret indeks
        new Customer_ByFirstAndLastName().Execute(store);

        return store;
    }

    static void Main()
    {
        var keepgoing = true;
        Console.WriteLine("RavenDB Test application\n");
        while (keepgoing)
        {
            Console.WriteLine("" +
                "0 = Get all Customers\n" +
                "1 = Get Customer by Id\n" +
                "2 = Create Customer\n" +
                "3 = Get All Orders\n" +
                "4 = Get Order by ID\n" +
                "5 = Create Order\n" +
                "6 = Change Firstname for customer");


            Console.Write("Write number : ");
            var select = Console.ReadLine();

            if (select == "0")
            {
                GetAllCustomers();
            }
            if (select == "1")
            {
                GetCustomerById();
            }
            if (select == "2")
            {
                CreateCustomer();
            }
            if (select == "3")
            {
                GetAllOrders();
            }
            if (select == "4")
            {
                GetOrderById();
            }
            if (select == "5")
            {
                CreateOrder();
            }
            if (select == "6")
            {
                ChangeCustomer();
            }

            keepgoing = DoMore(keepgoing);
        }
    }

    private static void GetOrderById()
    {
        Console.Write("Enter OrderID : ");
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        var order = session.Load<Order>(Console.ReadLine());

        Console.WriteLine($"OrderId=({order.Id})  Customer=({order.CustomerId})");
        Console.WriteLine($"    Shipping Adress : {order.ShippingAddress.Street} {order.ShippingAddress.number} {order.ShippingAddress.City}");
        Console.WriteLine("    Ordered Items");
        foreach (var item in order.Items)
        {
            Console.WriteLine("     " + item);
        }
    }
    private static void CreateOrder()
    {
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        var cust = Order.CreateOrder();
        session.Store(cust);
        session.SaveChanges();
    }
    private static void GetAllOrders()
    {
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        List<Order> orders = session.Query<Order>().ToList();

        foreach (var order in orders)
        {
            Console.WriteLine($"OrderId=({order.Id})  Customer=({order.CustomerId})");
            Console.WriteLine($"    Shipping Adress : {order.ShippingAddress.Street} {order.ShippingAddress.number} {order.ShippingAddress.City}");
            Console.WriteLine("    Ordered Items");
            foreach (var item in order.Items)
            {
                Console.WriteLine("     " + item);
            }
        }
    }
    private static void CreateCustomer()
    {
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        var cust = Customer.NewCust();
        session.Store(cust);
        session.SaveChanges();
    }
    private static void GetAllCustomers()
    {
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        List<Customer> emps = session.Query<Customer>().ToList();

        foreach (var emp in emps)
        {
            Console.WriteLine($"{emp.Id}    {emp.FirstName} {emp.LastName}");
        }
    }
    private static void GetCustomerById()
    {
        Console.Write("Enter ID : ");
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        var employee = session.Load<Customer>(Console.ReadLine());
        Console.WriteLine($"{employee.FirstName} {employee.LastName}");
    }
    private static bool DoMore(bool keepgoing)
    {
        Console.Write("Do more? Y/n : ");
        var awnser = Console.ReadLine().ToUpper();
        if (awnser == "N")
        {
            keepgoing = false;
        }

        return keepgoing;
    }
    private static void ChangeCustomer()
    {
        using var documentStore = CreateStore();
        using var session = documentStore.OpenSession();
        Console.Write("Enter Customer number : ");
        var customer = session.Load<Customer>(Console.ReadLine());

        Console.Write("New First Name");
        customer.FirstName = Console.ReadLine();
        session.SaveChanges();
    }
}