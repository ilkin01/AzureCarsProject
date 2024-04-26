using App.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Twilio.TwiML.Voice;
using WebUI.Models;

namespace WebUI.Helper
{
    public class HelperFunctions
    {

		

		public static bool IsEmpty(CarFilterViewModel car)
        {
            return
                string.IsNullOrEmpty(car.MarketPlaces) &&
                car.SeatCounts == null &&
                car.OwnerCounts==null&&
                string.IsNullOrEmpty(car.Cities) &&
                string.IsNullOrEmpty(car.Transmissions) &&
                string.IsNullOrEmpty(car.TractionTypes) &&
                MainsIsEmpty(car.Mains) &&
                EnginesIsEmpty(car.Engines) &&
                StatusesIsEmpty(car.Statuses) &&
                FinancesIsEmpty(car.Finances) &&
                car.MinHorsePower == 0 &&
                car.MaxHorsePower == 0 &&
                car.MinCarPrice == 0 &&
                car.MaxCarPrice == 0 &&
                car.MinKilometer == 0 &&
                car.MaxKilometer == 0;
        }

        private static bool MainsIsEmpty(Main main)
        {
            if (main == null)
                return true;

            return string.IsNullOrEmpty(main.Vendor) &&
                   string.IsNullOrEmpty(main.Model) &&
                   string.IsNullOrEmpty(main.SubModel) &&
                   string.IsNullOrEmpty(main.CarType) &&
                   main.Year == 0 &&
                   main.Kilometer == 0 &&
                   string.IsNullOrEmpty(main.Color) &&
                   (main.Cars == null || main.Cars.Count == 0);
        }

        private static bool EnginesIsEmpty(Engine engine)
        {
            if (engine == null)
                return true;

            return engine.EngineCapacity == 0 &&
                   engine.EnginePower == 0 &&
                   string.IsNullOrEmpty(engine.FuelType);
        }

        private static bool StatusesIsEmpty(Status status)
        {
            if (status == null)
                return true;

            return !status.IsHit && !status.IsPaint;
        }

        private static bool FinancesIsEmpty(Finance finance)
        {
            if (finance == null)
                return true;

            return finance.Price == 0 &&
                   string.IsNullOrEmpty(finance.CurrencyType) &&
                   !finance.IsCredit &&
                   !finance.IsBarter &&
                   !finance.IsGuarantee;
        }

        


    }


    //public string GenerateJwtToken(string username, int id, string phoneNumber)
    //{
    //	var issuer = builder.Configuration["Jwt:Issuer"];
    //	var audience = builder.Configuration["Jwt:Audience"];
    //	var key = Encoding.ASCII.GetBytes
    //	(Configuration["Jwt:Key"]);
    //	var tokenDescriptor = new SecurityTokenDescriptor
    //	{
    //		Subject = new ClaimsIdentity(new[]
    //		{
    //		new Claim("Id", Guid.NewGuid().ToString()),
    //		new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
    //		new Claim(JwtRegisteredClaimNames.Email, user.UserName),
    //		new Claim(JwtRegisteredClaimNames.Jti,
    //		Guid.NewGuid().ToString())
    //	 }),
    //		Expires = DateTime.UtcNow.AddMinutes(5),
    //		Issuer = issuer,
    //		Audience = audience,
    //		SigningCredentials = new SigningCredentials
    //		(new SymmetricSecurityKey(key),
    //		SecurityAlgorithms.HmacSha512Signature)
    //	};
    //	var tokenHandler = new JwtSecurityTokenHandler();
    //	var token = tokenHandler.CreateToken(tokenDescriptor);
    //	var jwtToken = tokenHandler.WriteToken(token);
    //	var stringToken = tokenHandler.WriteToken(token);
    //}



}
