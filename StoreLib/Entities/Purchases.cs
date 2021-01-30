using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Entities
{
    [Table("Purchases")]
    public class Purchases
    {
        public int Id { get; set; }
        public string Description { get; set; } 
        public string BuyersName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
