using App.Entities.Models;

namespace WebUI.Models
{
    public class CarFilterViewModel
    {
        //public List<string> MarketPlaces { get; set; } = new List<string>();
        //public List<int?> SeatCounts { get; set; } = new List<int?>();
        //public List<int?> OwnerCounts { get; set; } = new List<int?>();
        //public List<string> Cities { get; set; } = new List<string>();
        //public List<string> Transmissions { get; set; } = new List<string>();
        //public List<string> TractionTypes { get; set; } = new List<string>();
        //public List<Main> Mains { get; set; } = new List<Main>();
        //public List<Engine> Engines { get; set; } = new List<Engine>();
        //public List<Status> Statuses { get; set; } = new List<Status>();
        //public List<Finance> Finances { get; set; } = new List<Finance>();


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
