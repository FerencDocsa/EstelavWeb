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


        [BindProperty]
        public  DeleteItem deleteItem { get; set; }

        public class ItemLocalDesciption
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class DeleteItem
        {
            public  int Id { get; set; }
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
            if (_httpContextAccessor != null)
            {
                string fullCookie = _httpContextAccessor.HttpContext.Request.Cookies["Culture"];

                culture = fullCookie != null ? fullCookie.Split('|')[0].Split("=")[1] : "en-US";
            }
            else
            {
                culture = "en-US";
            }


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
            //Get Images List
            _desItems = _context.ImagesList.Where(c => c.ItemId == id).ToList();

            //Select Recommended Items
            _recItems = _context.Items.Where(c => c.InStock).OrderBy(c => Guid.NewGuid()).Take(4).ToList(); // .Where(x => x.InStock == true).Take(4).ToList();

            
            var itemDescriptionObject = _context.ItemsDescription.FirstOrDefault(c => c.Item.ItemId == id && c.Language == culture);
            if(itemDescriptionObject != null)
            {
                _itemName = itemDescriptionObject.Name;
                _itemDescription = itemDescriptionObject.Description;
            }



            return new PageResult();  
        }

        public IActionResult OnPostDeleteItem()
        {
            var getItemToDelete = _context.Items.FirstOrDefault(c => c.ItemId == deleteItem.Id);

            if (getItemToDelete != null)
            {
                _context.Items.Remove(getItemToDelete);
                _context.SaveChanges();
            }

            return RedirectToPage("/Index");
        }


    }
}
