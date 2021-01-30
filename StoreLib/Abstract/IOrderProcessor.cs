using StoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Abstract
{
    public interface IOrderProcessor
    {
        string ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
