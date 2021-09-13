using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Estelav.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Estelav.Pages.Panel
{
    [Authorize(Roles = "Admin")]
    public class ItemsManagerModel : PageModel
    {
        private readonly EstelavContext _context;

        [BindProperty]
        public UploadModel Upload { get; set; }
        public List<Categories> categoriesList { get; set; }

        [BindProperty]
        public IFormFile mainPhoto { get; set; }
        [BindProperty]
        public IFormFile[] descrPhotos { get; set; }

        public class UploadModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string NameRU { get; set; }
            
            [Required]
            public string NameCZ{ get; set; }


            [Required]
            public int CatergoryId { get; set; }

            //public IFormFile mainPhoto { get; set; }


            List<string> sizes { get; set; }

            public int Price { get; set; }

            public string Description { get; set; }
            public string DescriptionRU { get; set; }
            public string DescriptionCZ { get; set; }
        }

        public ItemsManagerModel(EstelavContext context)
        {
            _context = context;
        }

        public void OnGet(int? itemID = null)
        {
            categoriesList = _context.Categories.ToList();
            if(itemID != null)
            {
                ViewData["newItemAdded"] = itemID;
            }
        }

        public IActionResult OnPost()
        {
            if(Upload == null)
            {
                return Content("Not all fields was filled");
            }

            if (descrPhotos == null || descrPhotos.Length == 0)
            {
                return Content("File(s) not selected");
            }

            //Upload Main Photo
            Guid id = Guid.NewGuid();
            var photoNewName = id + "_" + mainPhoto.FileName;
            var pathToMain = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/merch", photoNewName);
            var streamMain = new FileStream(pathToMain, FileMode.Create);
            mainPhoto.CopyToAsync(streamMain);
            var dbPath = "/img/merch/" + photoNewName;

            //Add new Item to DB
            var newItem = new Items { CategoryId = Upload.CatergoryId,
                Name = Upload.Name, 
                Price = Upload.Price, 
                Amount = 50,
                InStock = true, 
                Description = Upload.Description, 
                ImageUrl = dbPath 
            };

            _context.Items.Add(newItem);
            _context.SaveChanges();

            //Multiple photos upload
            List<ImagesList> imList = new List<ImagesList>();
            
            foreach (IFormFile photo in descrPhotos)
            {
                Guid idMultiple = Guid.NewGuid();
                var photosNewName = idMultiple + "_" + photo.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/merch", photosNewName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);
                var dbMulPath = "img/merch/" + photosNewName;
                imList.Add(new ImagesList { ImageUrl = dbMulPath, ItemId = newItem.ItemId });              
            }

            _context.ImagesList.AddRange(imList);


            //Lang Decription 
            var descrCZ = new ItemsDescription { Description = Upload.DescriptionCZ, Language = "cs-CZ", Name = Upload.NameCZ, ItemId = newItem.ItemId };

            var descrRU = new ItemsDescription { Description =@Upload.DescriptionRU, Language = "ru-RU", Name = Upload.NameRU, ItemId = newItem.ItemId };
            

            _context.ItemsDescription.Add(descrCZ);
            //_context.Entry<ItemsDescription>(descrCZ).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _context.SaveChanges();

            _context.ItemsDescription.Add(descrRU);
            //_context.Entry<ItemsDescription>(descrRU).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _context.SaveChanges();


            return RedirectToPage("/Panel/ItemsManager", new { itemID = newItem.ItemId });
        }

    }
}
