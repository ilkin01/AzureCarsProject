using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IFilterService
    {
        CarFilter GetFilterDatas();
        //List<Car> GetFilteredCars(Filter filter);
        List<Post> GetFilteredCars(Filter filter);
    }
}
