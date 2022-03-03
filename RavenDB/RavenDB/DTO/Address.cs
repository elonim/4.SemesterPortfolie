
namespace RavenDB.DTO
{
    public class Address
    {
        public string Street { get; set; }
        public int number { get; set; }
        public string City { get; set; }

        public static Address CreateAddress()
        {
            var a = new Address();
            Console.Write("Enter Streetname: ");
            a.Street = Console.ReadLine();
            Console.Write("Enter Streetnumber: ");
            a.number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter City: ");
            a.City = Console.ReadLine();
            return a;
        }
    }
}
