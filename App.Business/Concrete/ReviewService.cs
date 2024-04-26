using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
	public class ReviewService : IReviewService
	{
		private IReviewDal _dal;

		public ReviewService(IReviewDal dal)
		{
			_dal = dal;
		}

		public void Add(Review review)
		{
			_dal.Add(review);
		}

		public void Delete(Review review)
		{
			_dal.Delete(review);
		}

		public Review Get()
		{
			return _dal.Get();
		}

		public List<Review> GetAll()
		{
			return _dal.GetList();
		}

		public void Update(Review review)
		{
			_dal.Update(review);
		}
	}
}
