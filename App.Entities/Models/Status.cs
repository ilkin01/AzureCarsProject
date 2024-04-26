using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Status:IEntity
    {
        public int Id { get; set; }
        public bool IsHit { get; set; }
        public bool IsPaint { get; set; }
        //public virtual Car Car { get; set; }
    }

}
