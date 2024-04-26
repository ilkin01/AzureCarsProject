using App.Core.DataAccess.EntityFramework;
using App.DataAccess.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Concrete.EfEntityFramework
{
	public class EfAutoSalonDal : EfEntityRepositoryBase<AutoSalon,CarDBContext>,IAutoSalonDal
	{
		public override List<AutoSalon> GetList(Expression<Func<AutoSalon, bool>> filter = null)
		{
			using (var context = new CarDBContext())
			{
				return filter == null
					? context.Set<AutoSalon>().Include("User").ToList()
					: context.Set<AutoSalon>().Where(filter).ToList();
			}
		}
	}
}
