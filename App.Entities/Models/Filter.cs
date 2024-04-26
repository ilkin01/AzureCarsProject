using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Filter
    {

        public string MarketPlaces { get; set; }
        public int? SeatCounts { get; set; }
        public int? OwnerCounts { get; set; }
        public string Cities { get; set; }
        public string Transmissions { get; set; }
        public string TractionTypes { get; set; }
        public Main Mains { get; set; }
        public Engine Engines { get; set; }
        public Status Statuses { get; set; }
        public Finance Finances { get; set; }


        public int MinHorsePower { get; set; }
        public int MaxHorsePower { get; set; }
        public int MinCarPrice { get; set; }
        public int MaxCarPrice { get; set; }
        public int MinKilometer { get; set; }
        public int MaxKilometer { get; set; }
    }
}
