using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Site.BLL.BusinessModels;
using Site.BLL.DTO;
using Site.BLL.Interfaces;
using Site.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.WebUI.Controllers
{
    public class RentalController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        IOrderService orderService;
        public RentalController(IOrderService os)
        {
            orderService = os;
        }
        [Authorize]
        public ActionResult Index(string carName="")
        {
            RentalModel rental = new RentalModel() { CarName = carName, DateTimeRented = DateTime.Now.Date, DateTimeReturned = DateTime.Now.Date.AddDays(1) };
            return View(rental);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Index(RentalModel rental)
        {
            rental.CarName = rental.CarName.ToUpperInvariant();
            var mapper = new MapperConfiguration(c => c.CreateMap<RentalModel, RentalDTO>()).CreateMapper();
            var rentalDTO = mapper.Map<RentalModel, RentalDTO>(rental);
            rentalDTO.ClientId = User.Identity.GetUserId();
            if (orderService.GetRentals().Where(c => c.ClientId == rentalDTO.ClientId && c.Status != "Complete" && c.Status!= "Unconfirmed").Count() != 0)
            {
                ModelState.AddModelError("", "You already have order");
                return View();
            }
            if (rentalDTO.DateTimeRented >= rentalDTO.DateTimeReturned)
            {
                ModelState.AddModelError("", "Choose correct date");
                return View();
            }
            if (orderService.GetCars().Where(c => c.Name == rental.CarName).FirstOrDefault() == null)
                ModelState.AddModelError("", "Car not found");
            else
            {
                rentalDTO.Status = "Pending";
                orderService.Add(rentalDTO);
            }
            return View();
        }
        public ActionResult Car(string CarName)
        {
            CarDTO carDTO = orderService.GetCars().Where(c => c.Name.Contains(CarName.ToUpperInvariant())).FirstOrDefault();
            if (carDTO == null)
                carDTO = orderService.GetCars().FirstOrDefault();
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, CarViewModel>()).CreateMapper();
            var car = mapper.Map<CarDTO, CarViewModel>(carDTO);
            return PartialView(car);
        }
        

        [AllowAnonymous]
        public JsonResult AutoCompleteCar(string temp="")
        {
            temp = temp.ToUpperInvariant();
            var result = orderService.GetCars().Select(c=>c.Name).Where(c=>c.Contains(temp));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult Price(DateTime start,DateTime end, string name)
        {
            return Json(orderService.GetPrice(start,end,name), JsonRequestBehavior.AllowGet);
        }
    }
}