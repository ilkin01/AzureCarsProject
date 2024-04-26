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
    public class EFImageDal:EfEntityRepositoryBase<Image,CarDBContext>,IImageDal
    {
		public override List<Image> GetList(Expression<Func<Image, bool>> filter = null)
		{
			using (var context = new CarDBContext())
			{
                //[ROH] Refactor Olunmamis Hali
                //return filter == null
                //	? context.Set<Image>().Include("Car").ToList()
                //	: context.Set<Image>().Where(filter).ToList();

                return filter == null
                    ? context.Set<Image>().ToList()
                    : context.Set<Image>().Where(filter).ToList();
            }
        }
	}
}
