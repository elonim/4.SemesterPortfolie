namespace RavenDB.DTO
{
    public class Customer
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public static Customer NewCust()
        {
            var cust = new Customer();
            Console.Write("Enter Firstname: ");
            cust.FirstName = Console.ReadLine();
            Console.Write("Enter Firstname: ");
            cust.LastName = Console.ReadLine();
            return cust;
        }
    }
}
