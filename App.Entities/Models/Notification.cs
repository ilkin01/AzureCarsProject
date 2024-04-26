using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    /// <summary>
    /// This class is for the user's notifications.
    /// </summary>
    public class Notification:IEntity
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }


    }
}
