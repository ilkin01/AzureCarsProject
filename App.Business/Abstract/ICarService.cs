using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car Get();

        void Add(Car car);
        void Update(Car car);
        void Delete(Car car);
    }
}
