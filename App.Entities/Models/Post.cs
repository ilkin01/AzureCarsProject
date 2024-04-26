using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Post:IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool? IsBoosted { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsPremium { get; set; }
        public bool? IsDeleted { get; set; }
        public int UserId { get; set; }
        public int ViewCount { get; set; }
        public int CarId { get; set; }


        public List<Review>? Reviews { get; set; }

        //[ForeignKey("UserId")]
        public virtual User User { get; set; }

        //[ForeignKey("Id")]
        public virtual Car Car { get; set; }
    }

}
