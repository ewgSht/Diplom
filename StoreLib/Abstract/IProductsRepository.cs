using StoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Purchases> Purchases { get; }
        void SavePurchase(Purchases purchase);
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}
