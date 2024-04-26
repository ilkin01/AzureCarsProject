using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IImageService
    {
        List<Image> GetAll();
        Image Get();

        void Add(Image image);
        void Update(Image image);
        void Delete(Image image);
    }
}
