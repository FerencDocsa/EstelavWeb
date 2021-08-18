using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using  Estelav.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Estelav.Pages.Cart
{
    public class CheckoutModel : PageModel
    {

        public DeliveryAdress address;

        public class DeliveryAdress
        {
            public string Address { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string Country { get; set; }

        }

        private static IServiceProvider _service;



        public CheckoutModel(IServiceProvider service)
        {
            _service = service;
        }


        public void OnGet()
        {
        }


        public IActionResult OnGetDelivery()
        {
            ISession session = _service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            var newOrder = new Orders({  })


        return Page();
        }

        public IActionResult OnGetPickup()
        {
            return Page();

        }
    }
}
