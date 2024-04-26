using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class FavoriteService : IFavoriteService
    {
        private IFavoriteDal favoriteDal;

        public FavoriteService(IFavoriteDal favoriteDal)
        {
            this.favoriteDal = favoriteDal;
        }

        public Favorite Get()
        {
            return favoriteDal.Get();
        }

        public List<Favorite> GetAll()
        {
            return favoriteDal.GetList();
        }
        public void Add(Favorite favorite)
        {
            favoriteDal.Add(favorite);
        }

        public void Update(Favorite favorite)
        {
            favoriteDal.Update(favorite);
        }

        public void Delete(Favorite favorite)
        {
            favoriteDal.Delete(favorite);
        }
    }
}
