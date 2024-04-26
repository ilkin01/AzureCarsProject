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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.DataAccess.Concrete.EfEntityFramework
{
    public class EFPostDal:EfEntityRepositoryBase<Post,CarDBContext>,IPostDal
    {
		public override List<Post> GetList(Expression<Func<Post, bool>> filter = null)
		{
			using (var context = new CarDBContext())
			{
				 var query = filter == null
					? context.Set<Post>().Include("User").Include("Car").Include(p=>p.Reviews).AsQueryable()
                : context.Set<Post>().Where(filter).AsQueryable();


        //        public int Id { get; set; }
        //public string MarketPlace { get; set; }
        //public string VINCode { get; set; }
        //public int? SeatCount { get; set; }
        //public int? OwnerCount { get; set; }
        //public string City { get; set; }
        //public string About { get; set; }
        //public string Transmission { get; set; }
        //public string TractionType { get; set; }
        //public int MainId { get; set; }
        //public Main Main { get; set; }
        //public int EngineId { get; set; }
        //public Engine Engine { get; set; }
        //public int StatusId { get; set; }
        //public Status Status { get; set; }
        //public int FinanceId { get; set; }
        //public Finance Finance { get; set; }
        //public int OwnerId { get; set; }
        //public User Owner { get; set; }
        //public List<Image> Images { get; set; }
        //public Post Post { get; set; }

        query = query.Include(p => p.Car.Main).Include(p=>p.Car.Engine).Include(p=>p.Car.Status).Include(p => p.Car.Finance)
                    .Include(p => p.Car.Owner).Include(p => p.Car.Images).IgnoreQueryFilters();
                

				return query.ToList();
            }
        }
	}
}
