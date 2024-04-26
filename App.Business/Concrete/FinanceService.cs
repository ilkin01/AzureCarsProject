using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.DataAccess.Concrete.EFEntityFramework;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
	public class FinanceService : IFinanceService
	{
		private IFinanceDal _service;

		public FinanceService(IFinanceDal service)
		{
			_service = service;
		}
		public Finance Get()
		{
			return _service.Get();
		}

		public List<Finance> GetAll()
		{
			return _service.GetList();
		}

        public void Add(Finance finance)
        {
            _service.Add(finance);
        }

        public void Update(Finance finance)
        {
            _service.Update(finance);
        }

        public void Delete(Finance finance)
        {
            _service.Delete(finance);
        }
    }
}
