using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post Get();

        void Add(Post post);
        void Update(Post post);
        void Delete(Post post);
    }
}
