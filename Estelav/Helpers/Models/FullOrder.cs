using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;
using Estelav.Pages.Panel;

namespace Estelav.Helpers.Models
{
    public class FullOrder
    {
        public int OrderId { get; set; }
        public double Total { get; set; }
        public string Address { get; set; }
        public Users User { get; set; }
        public List<OrderedItem> OrderedItems { get; set; }

    }
}
