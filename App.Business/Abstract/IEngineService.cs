using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IEngineService
    {
        List<Engine> GetAll();
        Engine Get();

        void Add(Engine engine);
        void Update(Engine engine);
        void Delete(Engine engine);
    }
}
