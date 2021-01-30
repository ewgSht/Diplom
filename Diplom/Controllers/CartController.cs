using Diplom.Models;
using StoreLib.Abstract;
using StoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diplom.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID ==
            productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID ==
            productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                string message=orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                if (message!="ok")
                {
                    TempData["MessageAnoutPoast"] = "Ошибка при отправлении письма на ваш E-Mail, но наш менеджер всёравно с вами свяжется.";

                }
                else
                {
                    TempData["MessageAnoutPoast"] = "Письмо с деталями заказа отправлено на ваш E-Mail, наш менеджер скоро с вами свяжется.";
                }
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}