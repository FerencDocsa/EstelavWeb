using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Panel
{
    public class ItemsManagerModel : PageModel
    {
        private readonly EstelavContext _context;

        public List<Categories> categoriesList { get; set; }

        public class UploadModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public int CatergoryId { get; set; }

            [Required]
            IFormFile mainPhoto { get; set; }

            [Required]
            IFormFile[] descrPhotos { get; set; }

            List<string> sizes { get; set; }

            public int Price { get; set; }



            public string Description { get; set; }

            public string MainImage { get; set; }

            public List<string> ImageList { get; set; }
        }

        public ItemsManagerModel(EstelavContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            categoriesList = _context.Categories.ToList();
        }

        public IActionResult OnPost(IFormFile[] photos, Items? product = null)
        {
            if (photos == null || photos.Length == 0)
            {
                return Content("File(s) not selected");
            }
            else
            {
                //product.Photos = new List<string>();
                foreach (IFormFile photo in photos)
                {
                    Guid id = Guid.NewGuid();

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/merch", id + "_" + photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    photo.CopyToAsync(stream);
                    //product.Photos.Add(photo.FileName);
                }
            }
            //ViewBag.product = product;
            return new OkResult();
        }

    }
}
