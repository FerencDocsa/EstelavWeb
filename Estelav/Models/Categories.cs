using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Items = new HashSet<Items>();
        }

        public int CatergoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual ICollection<Items> Items { get; set; }
    }
}
