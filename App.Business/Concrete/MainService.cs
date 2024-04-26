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
	public class MainService : IMainService
	{
		private IMainDal _service;

		public MainService(IMainDal service)
		{
			_service = service;
		}

		public Main Get()
		{
			return _service.Get();
		}

		public List<Main> GetAll()
		{
			return _service.GetList();
		}

        public void Add(Main main)
        {
            _service.Add(main);
        }

        public void Update(Main main)
        {
            _service.Update(main);
        }

        public void Delete(Main main)
        {
            _service.Delete(main);
        }
    }
}
