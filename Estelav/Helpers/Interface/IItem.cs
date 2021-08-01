using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estelav.Models;



namespace Estelav.Helpers.Interface
{
    public interface IItem
    {
        IEnumerable<Items> GetAll();

        Items GetById(int id);
    }
}
