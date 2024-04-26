using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IMainService
	{
		List<Main> GetAll();
		Main Get();

		void Add(Main main);
		void Update(Main main);
		void Delete(Main main);
	}
}
