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
	public class EFUserDal : EfEntityRepositoryBase<User, CarDBContext>, IUserDal
	{
		public override List<User> GetList(Expression<Func<User, bool>> filter = null)
		{
			using (var context = new CarDBContext())
			{
				IQueryable<User> query = context.Set<User>();

				if (filter != null)
				{
					query = query.Where(filter);
				}

				return query

					.Include(u => u.AutoSalons)
					.Include(u => u.OwnedCars)
					.Include(u => u.Posts)
					.ThenInclude(p => p.Car).ThenInclude(p => p.Main)
					.Include(u => u.Posts)
					.ThenInclude(p => p.Car).ThenInclude(p => p.Images)
					.Include(u => u.Posts)
					.ThenInclude(p => p.Car).ThenInclude(p => p.Engine)
					.Include(u => u.Posts)
					.ThenInclude(p => p.Car).ThenInclude(p => p.Status)
					.Include(u => u.Posts)
					.ThenInclude(p => p.Car).ThenInclude(p => p.Finance)
					.Include(u => u.Favorites)
					.ToList();
			}
		}
	}
}
