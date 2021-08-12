using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Estelav.Pages.Cart
{
    public class ShoppingCartModel : PageModel
    {
        private readonly EstelavContext _context;

        private static IServiceProvider _service;

        public IList<ShoppingCartItem> _shopCartItems { get; set; }

        public double _shoppingCartSum { get; set; }

        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        [BindProperty]
        public CheckoutCartModel checkout { get; set; }

        public class CheckoutCartModel
        {
            public string shoppingCartId { get; set; }
            public int shopingCartSummary { get; set; }
        }

        public ShoppingCartModel(EstelavContext context, IServiceProvider services)
        {
            _context = context;
            _service = services;

            GetCart();
            if (Id != null)
                RetrieveShoppingCartItems();
        }

        public IActionResult OnPost()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Cart/Checkout");
            }
            else
            {
                return RedirectToPage("/Account/Login", new { returnUrl = "", Message = "Login" });
            }
        }

        public void GetCart()
        {
            ISession session = _service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = _service.GetService<EstelavContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            Id = cartId;
        }

        public void RetrieveShoppingCartItems()
        {
            if(Id == null)
                GetCart();


            var items = _context.ShoppingCartItem
                .Where(session => session.ShoppingCartId == Id)
                .Include(c => c.ItemNavigation).ToList();

            var sum = _context.ShoppingCartItem
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.ItemNavigation.Price * c.Amount).Sum();

            _shopCartItems = items;
            _shoppingCartSum = sum;



        }

        public void OnGetAdd(int id, int? amount = 1)
        {
            //var item = _itemService.GetById(id);
            var item = _context.Items.FirstOrDefault(c => c.ItemId == id);
            //returnUrl = returnUrl.Replace("%2F", "/");
            bool isValidAmount = false;
            if (item != null)
            {
                isValidAmount = AddToCart(item, amount.Value);
            }

            //return Index();
        }

        public bool AddToCart(Items item, int amount) {
            if (Id == null)
                GetCart();


            if (item.InStock == false || item.Amount == 0)
            {
                return false;
            }

            var shoppingCartItem = _context.ShoppingCartItem.SingleOrDefault(
                s => s.ItemNavigation.ItemId == item.ItemId && s.ShoppingCartId == Id);

            var isValidAmount = true;
            if (shoppingCartItem == null)
            {
                if (amount > item.Amount)
                {
                    isValidAmount = false;
                }
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    ItemNavigation = item,
                    Amount = Math.Min(item.Amount, amount)
                };
                _context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                if (item.Amount - shoppingCartItem.Amount - amount >= 0)
                {
                    shoppingCartItem.Amount += amount;
                }
                else
                {
                    shoppingCartItem.Amount += (item.Amount - shoppingCartItem.Amount);
                    isValidAmount = false;
                }
            }


            _context.SaveChanges();
            return isValidAmount;
        }

        public int RemoveFromCart(Items item)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(
                s => s.ItemNavigation.ItemId == item.ItemId);
            int localAmmount = 0;
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
            return localAmmount;       
        }

        public IActionResult OnGetRemove(int id)
        {
            var item = _context.Items.FirstOrDefault(c => c.ItemId == id);
            if(item != null)
            {
                RemoveFromCart(item);
            }

            return Redirect("/Cart");
        }


        public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _context.ShoppingCartItem.Where(c => c.ShoppingCartId == Id)
                .Include(s => s.ItemNavigation);
        }

        public void ClearCart()
        {
            var cartItems = _context
                .ShoppingCartItem
                .Where(cart => cart.ShoppingCartId == Id);

            _context.ShoppingCartItem.RemoveRange(cartItems);
            _context.SaveChanges();
        }


        public void OnGet()
        {



        }
    }
}
