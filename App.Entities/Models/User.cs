using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Entities.Models
{
	public class User : IEntity
	{
		public int Id { get; set; }
		public string PhoneNumber { get; set; }
		public string Fullname { get; set; }
		public int PostCount { get; set; }
		public List<AutoSalon> AutoSalons { get; set; }
		[JsonIgnore]
		public List<Post> Posts { get; set; }
		public List<Car> OwnedCars { get; set; }
		public List<Favorite> Favorites { get; set; }
	}

}
