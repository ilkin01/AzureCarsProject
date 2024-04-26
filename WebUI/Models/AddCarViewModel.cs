using System.CodeDom;

namespace WebUI.Models
{
    public class AddCarViewModel
    {
        public AddCarViewModel()
        {
            for (int i = 1900; i < 2024; i++)
            {
                Years.Add(i.ToString());
            }

            for (int i =100; i < 16100; i+=100)
            {
                EngineCapacities.Add(i.ToString());
            }
        }
        public List<string> carBrands = new List<string>
        {
            "Toyota",
            "Ford",
            "Chevrolet",
            "Honda",
            "Nissan",
            "Volkswagen",
            "BMW",
            "Mercedes",
            "Audi",
            "Hyundai",
            "Kia",
            "Mazda",
            "Subaru",
            "Jeep",
            "Lexus",
            "Volvo",
            "Porsche",
            "Tesla",
            "Ferrari",
            "Lamborghini",
            "Maserati",
            "Aston Martin",
            "Jaguar",
            "Land Rover",
            "Mitsubishi",
            "Buick",
            "Cadillac",
            "GMC",
            "Chrysler",
            "Dodge",
            "Alfa Romeo",
            "Infiniti",
            "MINI",
            "Smart",
            "Saab",
            "Suzuki",
            "Lincoln",
            "Pontiac",
            "Saturn",
            "Hummer",
            "Acura",
            "Oldsmobile",
            "Scion",
            "Fiat",
            "Bentley",
            "Rolls-Royce",
            "Koenigsegg",
            "Bugatti",
            "McLaren",
            "Pagani",
            "Genesis",
            "Spyker",
            "Lotus",
            "Maybach",
            "Lancia",
            "Lada",
            "Abarth",
            "DeLorean",
            "Proton",
            "Zenvo",
            "Wuling",
            "BYD",
            "Geely",
            "Chery",
            "Great Wall",
            "Changan",
            "Foton",
            "JAC Motors",
            "Haval",
            "Dacia"
        };
        public List<string> carModels = new List<string>
        {
            "Camry",
            "Mustang",
            "Camaro",
            "Civic",
            "Altima",
            "Golf",
            "3 Series",
            "E-Class",
            "A4",
            "Elantra",
            "Soul",
            "MX-5",
            "Outback",
            "Wrangler",
            "RX",
            "XC60",
            "911",
            "Model S",
            "488 GTB",
            "Aventador",
            "Quattroporte",
            "DB11",
            "F-PACE",
            "Range Rover",
            "Outlander",
            "Enclave",
            "Escalade",
            "Sierra",
            "300",
            "Challenger",
            "Giulia",
            "Q50",
            "Cooper",
            "ForTwo",
            "9-3",
            "Swift",
            "Navigator",
            "Firebird",
            "Vue",
            "H2",
            "MDX",
            "Cutlass",
            "xB",
            "500",
            "Continental GT",
            "Phantom",
            "Agera",
            "Chiron",
            "720S",
            "Huayra",
            "G70",
            "C8",
            "Elise",
            "S-Class",
            "Delta",
            "Niva",
            "500",
            "DMC-12",
            "Saga",
            "TSR",
            "Hong Guang",
            "Tang",
            "Emgrand X7",
            "Tiggo 8",
            "Haval H6",
            "CS75",
            "Tunland",
            "S3",
            "F7",
            "Duster"
        };

        public List<string> Bans = new List<string>
        {
            "Sedan",
            "HatchBack",
            "OffRoad",
            "SUV",
            "Coupe"
        };

        public List<string> SeatCounts = new List<string>
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8+"
        };
        public List<string> FuelType = new List<string>{
            "Benzin",
            "Diesel",
            "Hybrid",
            "Gas",
            "Electro",
            "Plug In Hybrid"
            };
        public List<string> Years = new List<string>();

        public List<string> Tractions = new List<string>
        {
            "Front",
            "Back",
            "4v4"
        };
        public List<string> Transmissions = new List<string>
        {
            "Manual",
            "Automatic",
            "Robot",
            "Variator"
        };
        public List<string> Colors = new List<string>
        {
            "Black",
            "Red",
            "Yellow",
            "White",
            "Green",
            "Gray",
            "Wet Asphalt",
            "Brown"
        };
        public List<string> EngineCapacities = new List<string>();

        public List<string> OwnerCount = new List<string>
        {
            "1",
            "2",
            "3",
            "4 Ve Daha Artiq",
        };

        public  List<string> MarketPlace = new List<string>
        {
            "America",
            "Europe",
            "Dubai",
            "Korea",
            "Russia",
            "Japan",
            "Distributor",
            "Other"
        };
    }
}
