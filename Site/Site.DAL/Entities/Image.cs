using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Entities
{
    public class Image
    {
        [Key, ForeignKey("Car")]
        public int Id { get; set; }
        public string Name { get; set; } 
        public byte[] Picture { get; set; }
        public Car Car { get; set; }
    }
}
