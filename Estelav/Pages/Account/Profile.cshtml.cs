using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Helpers.Models;
using Estelav.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private static EstelavContext _context;
        public List<FullOrder> AllOrders = new List<FullOrder>();

        public Users curUser { get; set; }

        public ProfileModel(EstelavContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var user = _context.Users.FirstOrDefault(c => c.EmailAddress == User.Identity.Name);
            curUser = user;
            GetUserOrders();
        }

        private void GetUserOrders()
        {
            List<OrderedItem> orderedItems = new List<OrderedItem>();
            List<FullOrder> fullOrders = new List<FullOrder>();


            var orders = _context.Orders.Where(c => c.UserId == curUser.Id).ToList();
            foreach (var order in orders)
            {
                var carts = _context.ShoppingCartItem.Where(c => c.ShoppingCartId == order.CartId).ToList();


                foreach (var cart in carts)
                {
                    var item = _context.Items.FirstOrDefault(c => c.ItemId == cart.Item);

                    if (item != null)
                    {
                        orderedItems.Add(new OrderedItem
                        {
                            ItemId = item.ItemId,
                            Name = item.Name,
                            Amount = cart.Amount,
                            Price = item.Price,
                            Size = "",
                            ImageUrl = item.ImageUrl
                        });
                    }
                }

                fullOrders.Add(new FullOrder
                {
                    OrderId = order.Id,
                    OrderedItems = orderedItems,
                    User = null,
                    Address = order.DeliveryAddress,
                    Total = order.CartTotal
                });
                orderedItems = new List<OrderedItem>();


            }

            AllOrders = fullOrders.OrderByDescending(c => c.OrderId).ToList();



        }
    }
}