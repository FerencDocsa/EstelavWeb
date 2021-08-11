using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages
{
    public class ItemModel : PageModel
    {
        private readonly EstelavContext _context;
        public Items foundItem { get; set; }
        public IList<Items> _recItems { get; set; }
        public IList<ImagesList> _desItems { get; set; }
        private string culture { get; set; }
        public string _itemName { get; set; }
        public string _itemDescription { get; set; }


        public class ItemLocalDesciption
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public IHttpContextAccessor _httpContextAccessor { get; set; }

        public ItemModel(EstelavContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            GetCulture();
        }


        public void GetCulture()
        {
            string fullCookie = _httpContextAccessor.HttpContext.Request.Cookies["Culture"];
            culture = fullCookie.Split('|')[0].Split("=")[1];
        }

        public IActionResult OnGet(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            foundItem = _context.Items.FirstOrDefault(c => c.ItemId == id);
            if(foundItem == null)
            {
                return NotFound();
            }

            _desItems = _context.ImagesList.Where(c => c.ItemId == id).ToList();
            _recItems = _context.Items.Where(c => c.CategoryId == 1).Take(4).ToList();


            var itemDescriptionObject = _context.ItemsDescription.FirstOrDefault(c => c.Item.ItemId == id);
            if(itemDescriptionObject != null)
            {
                _itemName = itemDescriptionObject.Name;
                _itemDescription
            }



            return new PageResult();  
        }


    }
}
