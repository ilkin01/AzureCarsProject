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
    public class CarService : ICarService
    {
        private ICarDal carDal;

        public CarService(ICarDal carDal)
        {
            this.carDal = carDal;
        }

        public Car Get()
        {
            return carDal.Get();
        }

        public List<Car> GetAll()
        {
            return carDal.GetList();
        }

        public void Add(Car car)
        {
            carDal.Add(car);
        }

        public void Update(Car car)
        {
            carDal.Update(car);
        }

        public void Delete(Car car)
        {
            carDal.Delete(car);
        }
    }
}
