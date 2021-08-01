using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Helpers.Interface;
using Estelav.Models;

namespace Estelav.Helpers.Services
{
    public class ItemService : IItem
    {

        private readonly EstelavContext _context;

        public ItemService(EstelavContext context)
        {
            _context = context;
        }
        public Items GetById(int id)
        {
            return GetAll().FirstOrDefault(c => c.ItemId == id);
        }
        public IEnumerable<Items> GetAll()
        {
            return _context.Items;

        }
    }
}
