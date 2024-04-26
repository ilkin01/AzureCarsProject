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
	public class StatusService : IStatusService
	{
		private IStatusDal _service;

		public StatusService(IStatusDal service)
		{
			_service = service;
		}

		public Status Get()
		{
			return _service.Get();
		}

		public List<Status> GetAll()
		{
			return _service.GetList();
		}

        public void Add(Status status)
        {
            _service.Add(status);
        }

        public void Update(Status status)
        {
            _service.Update(status);
        }

        public void Delete(Status status)
        {
            _service.Delete(status);
        }
    }
}
