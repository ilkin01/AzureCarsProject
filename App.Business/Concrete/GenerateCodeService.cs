using App.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class GenerateCodeService : ICodeGeneratorService
    {
        private Random random = new Random();

        public string GenerateCode()
        {
            int code = random.Next(10000); // Generate a random integer between 0 and 9999
            return code.ToString("D4"); // Convert the code to a string with 4 digits
        }
    }
}
