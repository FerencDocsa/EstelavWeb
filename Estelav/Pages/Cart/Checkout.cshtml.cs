using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using  Estelav.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Estelav.Pages.Cart
{
    [Authorize]
    public class CheckoutModel : PageModel
    {

        [BindProperty]
        public DeliveryAdress DeliverTo { get; set; }

        private EstelavContext _context;

        public class DeliveryAdress
        {
            [Required]
            public string Address { get; set; }
            [Required]
            public string Street { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
            [Required]
            public string Country { get; set; }


        }

        private static IServiceProvider _service;
        private static string CartID { get; set; }
        private static Users UserID { get; set; }
        private static List<ShoppingCartItem> cartItems { get; set; }

        public CheckoutModel(EstelavContext context, IServiceProvider service)
        {
            _service = service;
            _context = context;

        }


        public void OnGet()
        {
            cartItems = _context.ShoppingCartItem.Where(c => c.ShoppingCartId == CartID).ToList();

            if (cartItems.Count == 0)
            {
                RedirectToPage("/Index");
            }

            CartID = GetSessionID();
            UserID = GetUserID();

        }

        private string GetSessionID()
        {
            ISession session = _service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            return cartId;
        }

        private Users GetUserID()
        {
            var userId = _context.Users.FirstOrDefault(c => c.EmailAddress == User.Identity.Name);
            return userId;
        }


        public IActionResult OnPostDelivery()
        {
            return RedirectToPage("/Cart/Confirm", new { o = ConfirmOrder(false).Id });
        }

        public IActionResult OnPostPickup()
        {
            return RedirectToPage("/Cart/Confirm", new { o = ConfirmOrder(true).Id });
        }


        public Orders ConfirmOrder(bool isPickup)
        {
            var sum = _context.ShoppingCartItem
                .Where(c => c.ShoppingCartId == CartID && c.Ordered != true)
                .Select(c => c.ItemNavigation.Price * c.Amount).Sum();



            var deliveryTo = DeliverTo.Address + ", " + DeliverTo.Street + ", " + DeliverTo.City + " " +
                             DeliverTo.PostCode + ", " + DeliverTo.Country;

            var newOrder = new Orders();

            if (!isPickup)
            {
                newOrder = new Orders
                {
                    CartTotal = sum,
                    UserId = UserID.Id,
                    CartId = CartID,
                    DeliveryAddress = deliveryTo,
                    IsPickup = false
                };
            }
            else
            {
                newOrder = new Orders
                {
                    CartTotal = sum,
                    UserId = UserID.Id,
                    CartId = CartID,
                    DeliveryAddress = "Na Příkopě 858/20, Prague",
                    IsPickup = true
                };
            }

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            //ClearCart();
            foreach (var item in cartItems)
            {
                _context.Attach(item);
                item.Ordered = true;
                _context.Entry(item).Property(c => c.Ordered).IsModified = true;
                _context.SaveChanges();
            }

            ISession session = _service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            session.SetString("CartId", Guid.NewGuid().ToString());

            _context.SaveChanges();

            return newOrder;
        }


    }
}
