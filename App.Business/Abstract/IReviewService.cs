using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IReviewService
	{
		List<Review> GetAll();
		Review Get();

		void Add(Review review);
		void Update(Review review);
		void Delete(Review review);

	}
}
