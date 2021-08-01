using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Item { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

        public virtual Items ItemNavigation { get; set; }
    }
}
