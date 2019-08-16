using System.Linq;
using Raven.Client.Indexes;
using RavenStore.Models;

namespace RavenStore.Infrastructure.Raven
{
    public class Products_ByCategory : AbstractIndexCreationTask<Product>
    {
        public Products_ByCategory()
        {
            Map = products => from product in products
                select new
                {
                    id = product.Id,
                    name = product.Name,
                    category = product.Category,
                    price = product.Price
                };
        }
    }
}