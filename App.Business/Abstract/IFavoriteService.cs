using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IFavoriteService
    {
        List<Favorite> GetAll();
        Favorite Get();

        void Add(Favorite favorite);
        void Update(Favorite favorite);
        void Delete(Favorite favorite);
    }
}
