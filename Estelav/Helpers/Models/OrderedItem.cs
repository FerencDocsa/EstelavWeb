using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estelav.Helpers.Models
{

    public class OrderedItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
    }

}
