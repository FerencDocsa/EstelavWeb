using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class Items
    {
        public Items()
        {
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public bool InStock { get; set; }
        public string ImageUrl { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
