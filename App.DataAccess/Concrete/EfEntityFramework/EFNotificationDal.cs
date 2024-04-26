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
    public class EFNotificationDal: EfEntityRepositoryBase<Notification, CarDBContext>, INotificationDal
    {
        //public override List<Image> GetList(Expression<Func<Image, bool>> filter = null)
        //{
        //    using (var context = new CarDBContext())
        //    {
        //        return filter == null
        //            ? context.Set<Image>().Include("Car").ToList()
        //            : context.Set<Image>().Where(filter).ToList();
        //    }
        //}
        public override List<Notification> GetList(Expression<Func<Notification, bool>> filter = null)
        {
            using(var context = new CarDBContext())
            {
                return filter==null
                    ? context.Set<Notification>().Include(n=>n.User).ToList()
                    : context.Set<Notification>().Where(filter).ToList();

            }
        }
    }
}
