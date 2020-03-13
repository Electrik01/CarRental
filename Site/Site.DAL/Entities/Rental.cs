using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.DAL.Entities
{
    /// <summary>
    /// Сущность аренды
    /// Хранит данные о покупателе, машине, времени выдачи-возврата
    /// </summary>
    public class Rental
    {
        public int Id { get; set; }
        public ClientProfile ClientProfile{ get; set; }
        public Car Car { get; set; }
        public DateTime DateTimeRented { get; set; }
        public DateTime DateTimeReturned { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
