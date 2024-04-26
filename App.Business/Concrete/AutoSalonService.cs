using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class AutoSalonService : IAutoSalonService
    {
        private IAutoSalonDal autoSalonDal;

        public AutoSalonService(IAutoSalonDal autoSalonDal)
        {
            this.autoSalonDal = autoSalonDal;
        }

        public AutoSalon Get()
        {
            return autoSalonDal.Get();
        }

        public List<AutoSalon> GetAll()
        {
            return autoSalonDal.GetList();
        }

        public void Add(AutoSalon autoSalon)
        {
            autoSalonDal.Add(autoSalon);
        }

        public void Update(AutoSalon autoSalon)
        {
            autoSalonDal.Update(autoSalon);
        }

        public void Delete(AutoSalon autoSalon)
        {
            autoSalonDal.Delete(autoSalon);
        }
    }
}
