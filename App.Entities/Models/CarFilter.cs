using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class CarFilter
    {
        public List<string> MarketPlace { get; set; } = new List<string>();
        public List<int?> SeatCount { get; set; } = new List<int?>();
        public List<int?> OwnerCount { get; set; } = new List<int?>();
        public List<string> City { get; set; } = new List<string>();
        public List<string> Transmission { get; set; } = new List<string>();
        public List<string> TractionType { get; set; } = new List<string>();
        public List<Main> Main { get; set; } = new List<Main>();
        public List<Engine> Engine { get; set; } = new List<Engine>();
        public List<Status> Status { get; set; } = new List<Status>();
        public List<Finance> Finance { get; set; } = new List<Finance>();


        // Huseyn: Menim Fikrimce Bu Formada Yazilis Suret Cehetden Daha Yavasliq verecek
        // Cunku Datalari Alan Zaman Bir Dovr Daha Acib Datalari O Formada "Filters"-e Elave Elemeliyik

        //public List<Filter> Filters { get; set; }

    }

    //public class Filter
    //{
    //    public string MarketPlace { get; set; } 
    //    public int? SeatCount { get; set; } 
    //    public int? OwnerCount { get; set; } 
    //    public string City { get; set; } 
    //    public string Transmission { get; set; } 
    //    public string TractionType { get; set; } 
    //    public Main Main { get; set; } 
    //    public Engine Engine { get; set; }
    //    public Status Status { get; set; }
    //    public Finance Finance { get; set; }
    //}
}
