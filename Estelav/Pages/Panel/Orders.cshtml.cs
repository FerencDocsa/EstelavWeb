using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Helpers.Models;
using Estelav.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Estelav.Pages.Panel
{
    [Authorize(Roles = "Admin")]
    public class OrdersModel : PageModel
    {

        public static EstelavContext _context;
        public List<FullOrder> AllOrders = new List<FullOrder>();
        public List<Users> UsersList = new List<Users>();


        public OrdersModel(EstelavContext context)
        {
            _context = context;
            Fill();
        }

        void Fill()
        {
            List<OrderedItem> orderedItems = new List<OrderedItem>();
            List<FullOrder> fullOrders = new List<FullOrder>();

            var orders = _context.Orders.ToList();

            foreach (var order in orders)
            {
                var carts = _context.ShoppingCartItem.Where(c => c.ShoppingCartId == order.CartId).ToList();
                var user = _context.Users.FirstOrDefault(c => c.Id == order.UserId);

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
                    User = user,
                    Address = order.DeliveryAddress,
                    Total = order.CartTotal
                });
                orderedItems = new List<OrderedItem>();




            }

            AllOrders = fullOrders.OrderByDescending(c => c.OrderId).ToList();

        }




        public void OnGet()
        {
        }
    }
}
