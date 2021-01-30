using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите имя адресс доставки")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите город доставки")]
        public string City { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите страну доставки")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите Email")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Введеный Email адресс не корректен")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите ваш телефон")]
        public string Phone { get; set; }
    }
}
