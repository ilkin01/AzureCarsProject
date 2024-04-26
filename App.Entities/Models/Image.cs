using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Image:IEntity
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

    }
}
