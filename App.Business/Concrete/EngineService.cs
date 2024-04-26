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
    public class EngineService : IEngineService
    {
        private IEngineDal engineDal;

        public EngineService(IEngineDal engineDal)
        {
            this.engineDal = engineDal;
        }

        public Engine Get()
        {
            return engineDal.Get();
        }

        public List<Engine> GetAll()
        {
            return engineDal.GetList();
        }

        public void Add(Engine engine)
        {
            engineDal.Add(engine);
        }

        public void Update(Engine engine)
        {
            engineDal.Update(engine);
        }

        public void Delete(Engine engine)
        {
            engineDal.Delete(engine);
        }
    }
}
