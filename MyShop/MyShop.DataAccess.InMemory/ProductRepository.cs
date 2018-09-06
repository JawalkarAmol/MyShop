using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = Cache["products"] as List<Product>;
            if(products ==null)
            {
                products = new List<Product>();
            }

        }
        public void Commit()
        {
            Cache["products"] = products;
        }

        public void Insert(Product P)
        {
            products.Add(P);
        }

        public void Update ( Product product)
        {
            Product ProductsToUpdate = products.Find(p=>p.Id==product.Id );
            if (ProductsToUpdate!=null)
            {
                ProductsToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }

        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product!=null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }

        }
      
    }
}
