using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class ItemsDescription
    {
        public int ItemId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DescrId { get; set; }

        public virtual Items Item { get; set; }
    }
}
