using App.Business.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class UserHelperService : IUserHelperService
    {


        public bool IsSignUpValid(string phoneNumber, string fullName)
        {
            bool validation = true;
            validation = phoneNumber.StartsWith("051") || phoneNumber.StartsWith("050") || phoneNumber.StartsWith("055")
                ||phoneNumber.StartsWith("070") || phoneNumber.StartsWith("077");
            validation = phoneNumber.Length == 9;
            validation = fullName.Split(' ').Length == 2;
            return validation;
        }
    }
}
