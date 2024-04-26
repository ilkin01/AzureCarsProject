using App.Entities.Models;

namespace WebUI.Models
{
    public class CarViewModel
    {
        public string MarketPlace { get; set; }
        public string VINCode { get; set; }
        public int? SeatCount { get; set; }
        public int? OwnerCount { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public string Transmission { get; set; }
        public string TractionType { get; set; }
        public int MainId { get; set; }
        public Main Main { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int FinanceId { get; set; }
        public Finance Finance { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public List<Image> Images { get; set; }
        public Post Post { get; set; }
    }
}
