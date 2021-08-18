using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Estelav.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace Estelav.Pages
{
    public class ItemsModel : PageModel
    {

        private readonly EstelavContext _context;
        public List<Models.Items> items { get; set; }
        public string _categoryName { get; set; }
        public string _categoryDescription { get; set; }
        public int _itemsCount { get; set; }
        public string _itemName { get; set; }
        private string culture { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }

        public ItemsModel(EstelavContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContextAccessor = httpContext;
        }

        public void GetCulture()
        {
            string fullCookie = _httpContextAccessor.HttpContext.Request.Cookies["Culture"];
            culture = fullCookie.Split('|')[0].Split("=")[1];
        }


        public IActionResult OnGet(int? id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToPage("/Index");
            }

            var category = _context.Categories.FirstOrDefault(c => c.CatergoryId == id);
            items = _context.Items.Where(c => c.CategoryId == id).OrderByDescending(o => o.InStock).ToList();
            _itemsCount = items.Count;

            if (category == null)
            {
                return RedirectToPage("/Index");
            }

            var cultureGet = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            if (cultureGet == "en")
            {
                _categoryName = category.CategoryName;
            }

            if (cultureGet == "cs")
            {
                _categoryName = category.CategoryNameCz;
            }
            if (cultureGet == "ru")
            {
                _categoryName = category.CategoryNameRu;
            }
            _categoryDescription = category.CategoryDescription;




            var itemDescriptionObject = _context.ItemsDescription.FirstOrDefault(c => c.Item.ItemId == id && c.Language == culture);
            if (itemDescriptionObject != null)
            {
                _itemName = itemDescriptionObject.Name;
            }


            return Page();


        }
    }
}