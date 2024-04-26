using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        User Get();

        void Add(User user);
        void Update(User user);
        void Delete(User user);
        User Exists(string phoneNumber);

    }
}
