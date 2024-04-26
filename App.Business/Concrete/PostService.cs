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
    public class PostService : IPostService
    {
        private IPostDal postDal;

        public PostService(IPostDal postDal)
        {
            this.postDal = postDal;
        }

        public Post Get()
        {
            return postDal.Get();
        }

        public List<Post> GetAll()
        {
            return postDal.GetList();
        }

        public void Add(Post post)
        {
            postDal.Add(post);
        }

        public void Update(Post post)
        {
            postDal.Update(post);
        }

        public void Delete(Post post)
        {
            postDal.Delete(post);
        }
    }
}
