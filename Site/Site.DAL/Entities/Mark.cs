﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Entities
{
     public class Mark
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDel { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}