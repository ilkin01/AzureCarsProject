using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IStatusService
	{
		List<Status> GetAll();
		Status Get();

		void Add(Status status);
		void Update(Status status);
		void Delete(Status status);
	}
}
