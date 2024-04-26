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
	public class EFCarDal : EfEntityRepositoryBase<Car, CarDBContext>, ICarDal
	{
		public override List<Car> GetList(Expression<Func<Car, bool>> filter = null)
		{
			using (var context = new CarDBContext())
			{
                //[ROH] Refactor Olunmamis Hali
                //return filter == null
                //	? context.Set<Car>().Include(a => a.Main).Include(a => a.Engine).Include(a => a.Finance)
                //	.Include(a => a.Owner).Include(a => a.Images).Include(a => a.Post).Include(a=>a.Status).ToList()
                //	: context.Set<Car>().Where(filter).ToList();

                return filter == null
                    ? context.Set<Car>().Include(a => a.Main).Include(a => a.Engine).Include(a => a.Finance)
                    .Include(a => a.Owner).Include(a => a.Images).Include(a => a.Status).ToList()
                    : context.Set<Car>().Where(filter).ToList();
            }
        }
	}
}
