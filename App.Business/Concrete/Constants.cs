using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public static class Constants
    {
        public static string UserNotFound { get; set; } = "User Does Not Exist";
        public static string OTPMessage { get; set; } = "We've sent One Time Password (OTP) to the your mobile number. Please enter it to complete verification";
        public static string EmptyData { get; set; } = "Please fill in all the required fields.";
    }
}
