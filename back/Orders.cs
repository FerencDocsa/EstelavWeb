using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Estelav.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string CartId { get; set; }
        public int CartTotal { get; set; }
        public string DeliveryAddress { get; set; }
        public bool? IsPickup { get; set; }
    }
}
