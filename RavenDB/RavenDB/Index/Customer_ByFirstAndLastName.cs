using Raven.Client.Documents.Indexes;
using RavenDB.DTO;

namespace RavenDB.Index
{
    public class Customer_ByFirstAndLastName : AbstractIndexCreationTask<Customer>
    {
        public Customer_ByFirstAndLastName()
        {
            Map = customers => from customer in customers
                               select new
                               {
                                   FirstName = customer.FirstName,
                                   LastName = customer.LastName
                               };
        }
    }
}
