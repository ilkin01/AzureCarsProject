using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App.Entities.Models
{
    public class AutoSalon:IEntity
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string Hours { get; set; }
        public string Location { get; set; }
        public bool IsOfficialDistributor { get; set; }
        public string ImageLink { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

}
