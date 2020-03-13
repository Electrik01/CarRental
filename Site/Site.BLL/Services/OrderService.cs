using Site.BLL.DTO;
using Site.DAL.Entities;
using Site.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Site.BLL.Interfaces;
using Site.BLL.BusinessModels;

namespace Site.BLL.Services
{
    public class OrderService : IOrderService
    {
     

        public IUnitOfWork db { get; set; }
        public OrderService(IUnitOfWork uow)
        {
            db = uow;
        }
        public void UpdateRental(RentalDTO rent)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RentalDTO, Rental>()).CreateMapper();
            Rental rental = mapper.Map<RentalDTO, Rental>(rent);
            db.Rentals.Update(rental);
            db.Save();
        }
        public IEnumerable<MarkDTO> GetMarks()
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Mark, MarkDTO>()).CreateMapper();
            var marks = mapper.Map<IEnumerable<Mark>, IEnumerable<MarkDTO>>(db.Marks.GetAll());
            return marks;
        }
        public IEnumerable<CarTypeDTO> GetCarTypes()
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<CarType, CarTypeDTO>()).CreateMapper();
            var carTypes = mapper.Map<IEnumerable<CarType>, IEnumerable<CarTypeDTO>>(db.CarTypes.GetAll());
            return carTypes;
        }
        public IEnumerable<RentalDTO> GetRentals()
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<Rental, RentalDTO>()
                                               .ForMember(r=>r.ClientId, opt=>opt.MapFrom(ro=>ro.ClientProfile.Id)))
                                               .CreateMapper();
            var rentals = mapper.Map<IEnumerable<Rental>, List<RentalDTO>>(db.Rentals.GetAll());
            return rentals;
        }
        public IEnumerable<CarDTO> GetCars()
        {
            IEnumerable<Car> carsDAL = db.Cars.GetAll().Where(c => c.IsDel != true);
            PriceBLL price = new PriceBLL();
            var mapper = new MapperConfiguration(c => c.CreateMap<Car, CarDTO>()
                                                .ForMember(co=>co.CarTypeS,opt=>opt.MapFrom(cn=>cn.CarType.Name))
                                                .ForMember(co=>co.MarkS,opt=>opt.MapFrom(cn=>cn.Mark.Name))
                                                .ForMember(co=>co.Picture,opt=>opt.MapFrom(cn=>cn.Image.Picture))
                                                .ForMember(co => co.PictureName, opt => opt.MapFrom(cn => cn.Image.Name)))
                                                .CreateMapper();
            var cars = mapper.Map<IEnumerable<Car>, List<CarDTO>>(carsDAL);
            foreach (var c in cars)
                c.PriceList = price.GetAll(c.Price);
            
            return cars;
        }
        public void Add(CarDTO carDTO)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, Car>()).CreateMapper();
            Car car = mapper.Map<CarDTO, Car>(carDTO);
            car.Image = new Image()
            {
                Name = carDTO.PictureName,
                Picture = carDTO.Picture
            };
            if (db.CarTypes.Find(c => c.Name == carDTO.CarTypeS).FirstOrDefault() == null)
                car.CarType = new CarType { Name = carDTO.CarTypeS };
            else
                car.CarType = db.CarTypes.Find(c => c.Name == carDTO.CarTypeS).FirstOrDefault();
            if (db.Marks.Find(c => c.Name == carDTO.MarkS).FirstOrDefault() == null)
                car.Mark = new Mark { Name = carDTO.MarkS };
            else
                car.Mark = db.Marks.Find(c => c.Name == carDTO.MarkS).FirstOrDefault();
            var v = db.Cars.Find(c => c.Name == car.Name).FirstOrDefault();
            if (db.Cars.Find(c => c.Name == car.Name).FirstOrDefault() == null)
            {
                db.Cars.Create(car);
                db.Save();
            }
        }
        public void Add(RentalDTO rental)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RentalDTO, Rental>()).CreateMapper();
            Rental rentaln = mapper.Map<RentalDTO, Rental>(rental);
            rentaln.Car = db.Cars.Find(c => c.Name == rental.CarName).FirstOrDefault();
            rentaln.ClientProfile = db.ClientManager.GetProfiles().Where(c => c.Id == rental.ClientId).FirstOrDefault();
            db.Rentals.Create(rentaln);
            db.Save();
        }
        public double GetPrice(DateTime start, DateTime end, string name)
        {
            PriceBLL price = new PriceBLL();
            return price.GetPrice(start, end, db.Cars.GetAll()
                                             .Where(c => c.Name ==  name)
                                             .FirstOrDefault().Price);
        }
        public void DeleteCar(int id)
        {
            db.Cars.Delete(id);
        }
        public void UpdateCar(CarDTO car)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, Car>()).CreateMapper();
            Car car_up = mapper.Map<CarDTO, Car>(car);
            if (db.Marks.Find(m => m.Name == car.MarkS).Count() == 0)
            {
                car_up.Mark = new Mark { Name = car.MarkS };
            }
            else
                car_up.Mark = db.Marks.Find(m => m.Name == car.MarkS.ToUpper()).FirstOrDefault();
            if (db.CarTypes.Find(m => m.Name == car.CarTypeS).Count() == 0)
            {
                car_up.CarType = new CarType { Name = car.CarTypeS };

            }
            else
                car_up.CarType = db.CarTypes.Find(m => m.Name == car.CarTypeS.ToUpper()).FirstOrDefault();
            db.Cars.Update(car_up);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
