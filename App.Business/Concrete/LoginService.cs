using App.Business.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class LoginService : ILoginService
    {
        private UserService _userService = new UserService(new EFUserDal());
        private SmsSenderService _smsSenderService = new SmsSenderService();
        private GenerateCodeService _generateCodeService = new GenerateCodeService();

        public User Login(string phoneNumber)
        {
            if (phoneNumber != null)
            {
                var user = _userService.GetUser(phoneNumber);
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            return null;
        }
    }
}
