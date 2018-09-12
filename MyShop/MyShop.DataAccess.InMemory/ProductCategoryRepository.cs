using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache Cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = Cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }

        }
        public void Commit()
        {
            Cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory P)
        {
            productCategories.Add(P);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductCategoriesToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (ProductCategoriesToUpdate != null)
            {
                ProductCategoriesToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

    }

}