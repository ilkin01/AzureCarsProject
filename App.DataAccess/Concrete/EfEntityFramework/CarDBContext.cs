using App.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess.Concrete.EfEntityFramework
{
	public class CarDBContext : DbContext
	{
		public CarDBContext()
		{
		}

		public CarDBContext(DbContextOptions<CarDBContext>options)
			:base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
			}
		}

		public DbSet<AutoSalon> AutoSalons { get; set; }
		public DbSet<Car> Cars { get; set; }
		public DbSet<Engine> Engines { get; set; }
		public DbSet<Finance> Finances { get; set; }
		public DbSet<App.Entities.Models.Image> Images { get; set; }
		public DbSet<Main> Mains { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }


    }
}
