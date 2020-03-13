using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.DTO
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string CarName { get; set; }
        public DateTime DateTimeRented { get; set; }
        public DateTime DateTimeReturned { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
