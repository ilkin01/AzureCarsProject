﻿using App.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Favorite:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
