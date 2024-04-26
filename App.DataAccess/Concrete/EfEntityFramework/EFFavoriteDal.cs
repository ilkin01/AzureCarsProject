using App.Core.DataAccess.EntityFramework;
using App.DataAccess.Abstract;
using App.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Concrete.EfEntityFramework
{
    public class EFFavoriteDal:EfEntityRepositoryBase<Favorite,CarDBContext>,IFavoriteDal
    {

        public override List<Favorite> GetList(Expression<Func<Favorite, bool>> filter = null)
        {
            using (var context = new CarDBContext())
            {
                //return filter == null
                //    ? context.Set<Favorite>().Include(a => a.Post).Include(a => a.User).ToList()
                //    : context.Set<Favorite>().Where(filter).ToList();
                var query = filter == null
                    ? context.Set<Favorite>().Include(a=>a.Post).Include(a=>a.User).AsQueryable()
                : context.Set<Favorite>().Where(filter).AsQueryable();


                query = query.Include(f => f.Post.Car).Include(p => p.Post.Car.Main).Include(p => p.Post.Car.Engine)
                    .Include(p => p.Post.Car.Status).Include(p => p.Post.Car.Finance)
                    .Include(p => p.Post.Car.Owner).Include(p => p.Post.Car.Images).Include(f => f.Post.User).Include(f=>f.Post.User.Favorites).IgnoreQueryFilters();
                

                return query.ToList();
            }
        }
    }
}
