using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IAutoSalonService
    {
        List<AutoSalon> GetAll();
        AutoSalon Get();

        void Add(AutoSalon autoSalon);
        void Update(AutoSalon autoSalon);
        void Delete(AutoSalon autoSalon);
    }
}
