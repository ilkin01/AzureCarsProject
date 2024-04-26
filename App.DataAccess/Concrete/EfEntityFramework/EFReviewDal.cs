using App.Core.DataAccess.EntityFramework;
using App.DataAccess.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Concrete.EfEntityFramework
{
	public class EFReviewDal: EfEntityRepositoryBase<Review, CarDBContext>, IReviewDal
	{
	}
}
