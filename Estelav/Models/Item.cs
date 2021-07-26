using System;
using System.Collections.Generic;

#nullable disable

namespace Estelav.Models
{
    public partial class Item
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int? State { get; set; }

        public virtual Category Category { get; set; }
    }
}
