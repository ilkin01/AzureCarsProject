using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IFinanceService
	{
		List<Finance> GetAll();
		Finance Get();

		void Add(Finance finance);
		void Update(Finance finance);
		void Delete(Finance finance);
	}
}
