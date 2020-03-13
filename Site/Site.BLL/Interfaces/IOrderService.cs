using Site.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.BLL.Interfaces
{
    public interface IOrderService
    {
        void UpdateRental(RentalDTO rent);
        IEnumerable<RentalDTO> GetRentals();
        IEnumerable<CarDTO> GetCars();
        IEnumerable<CarTypeDTO> GetCarTypes();
        IEnumerable<MarkDTO> GetMarks();
        void Add(CarDTO carDTO);
        void Add(RentalDTO rental);
        void UpdateCar(CarDTO car);
        void DeleteCar(int id);
        double GetPrice(DateTime start, DateTime end, string name);
        void Dispose();
    }
}
