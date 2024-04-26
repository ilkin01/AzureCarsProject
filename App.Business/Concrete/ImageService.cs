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
    public class ImageService : IImageService
    {
        private IImageDal imageDal;

        public ImageService(IImageDal imageDal)
        {
            this.imageDal = imageDal;
        }

        public Image Get()
        {
            return imageDal.Get();
        }

        public List<Image> GetAll()
        {
            return imageDal.GetList();
        }

        public void Add(Image image)
        {
            imageDal.Add(image);
        }

        public void Update(Image image)
        {
            imageDal.Update(image);
        }

        public void Delete(Image image)
        {
            imageDal.Delete(image);
        }
    }
}
