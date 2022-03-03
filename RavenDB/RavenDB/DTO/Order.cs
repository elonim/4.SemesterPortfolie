namespace RavenDB.DTO
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public Address ShippingAddress { get; set; }
        public List<string> Items { get; set; } = new List<string>();

        public static Order CreateOrder()
        {
            var o = new Order();
            Console.Write("Enter CustomerId: ");
            o.CustomerId = Console.ReadLine();
            o.ShippingAddress = Address.CreateAddress();
            Console.Write("Enter Ordered Items: ");
            var i = true;
            do
            {
                Console.Write("Add new item: ");
                o.Items.Add(Console.ReadLine());

                Console.Write("Add new item? Y/n : ");
                var awnser = Console.ReadLine().ToUpper();
                if (awnser == "N")
                {
                    i = false;
                }

            } while (i);
            return o;
        }
    }
}
