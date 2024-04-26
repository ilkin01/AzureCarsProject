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
    public class UserService : IUserService
    {
        private IUserDal userDal;
        private IAesEncryptionServiceV2 _aesEncryptionServiceV2;



		public UserService(IUserDal userDal, IAesEncryptionServiceV2 aesEncryptionServiceV2 = null)
		{
			this.userDal = userDal;
			_aesEncryptionServiceV2 = aesEncryptionServiceV2;
		}

		public User Get()
        {
            return userDal.Get();
        }

        public List<User> GetAll()
        {
            return userDal.GetList();
        }
        public User GetUser(string phoneNumber)
        {
            return userDal.GetList().SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

        public void Add(User user)
        {
            userDal.Add(user);
        }

        public void Update(User user)
        {
            userDal.Update(user);
        }

        public void Delete(User user)
        {
            userDal.Delete(user);
        }

        public User Exists(string phoneNumber)
        {
            return userDal.GetList().FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        }
    }
}
