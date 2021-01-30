using StoreLib.Abstract;
using StoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Concrete
{
    public class EmailSettings
    {
        public string MailFromAddress = "for_diplom@mail.kz";
        public bool UseSsl = true;
        public string Password = "Qq12345678";
        public string ServerName = "post.mail.kz ";
        public int ServerPort = 587;
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private IProductRepository repository;
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings, IProductRepository productRepository)
        {
            emailSettings = settings;
            repository = productRepository;
        }
        public string ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                StringBuilder body = new StringBuilder()
                .AppendLine("Ваш заказ:\r\n")
                .AppendLine("Предметы:\r\n");
                int i = 0;
                foreach (var line in cart.Lines)
                {
                    i++;
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendLine("Позиция №"+i+":"+line.Quantity.ToString()+"" + line.Product.Name+"*"+ line.Product.Price+"="+ subtotal.ToString()+ "\r\n");
                }

                body.AppendFormat("Общая сумма заказа: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Доставка:")
                .AppendLine(shippingInfo.Name)
                .AppendLine(shippingInfo.Adress)
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.Country)
                .AppendLine(shippingInfo.Email)
                .AppendLine(shippingInfo.Phone);

                StringBuilder description = new StringBuilder();
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    description.AppendLine(""+line.Quantity.ToString() + "шт " + line.Product.Name + "*" + line.Product.Price + "=" + subtotal.ToString());
                }
                description.Append("\n\t\tОбщая сумма заказа:" + cart.ComputeTotalValue());
                Purchases purchases = new Purchases();
                purchases.Description = description.ToString();
                purchases.BuyersName = shippingInfo.Name;
                purchases.Phone = shippingInfo.Phone;
                purchases.Email = shippingInfo.Email;
                purchases.Date = DateTime.Now;
                repository.SavePurchase(purchases);


                MailAddress from = new MailAddress(emailSettings.MailFromAddress, "Мебельный магазин");

                MailAddress to = new MailAddress(shippingInfo.Email);

                MailMessage m = new MailMessage(from, to);

                m.Subject = " Ваш заказ";

                m.Body = body.ToString();

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(emailSettings.ServerName, emailSettings.ServerPort);

                try
                {
                    smtp.Credentials = new NetworkCredential(emailSettings.MailFromAddress, emailSettings.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    return "ok";
                }
                catch (Exception)
                {

                    return "error";
                }
               
            }

        }
    }
}
