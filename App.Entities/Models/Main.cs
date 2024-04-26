using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Main:IEntity
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string CarType { get; set; }
        public int Year { get; set; }
        public int Kilometer { get; set; }
        public string Color { get; set; }

        public List<Car> Cars { get; set; }
    }

}
