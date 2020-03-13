using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
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
    [Authorize(Roles ="manager")]
    public class ManagerController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        IOrderService orderService;
        public ManagerController(IOrderService os)
        {
            orderService = os;
        }
        public ActionResult Index()
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RentalDTO, RentalModel>()).CreateMapper();
            var rentals = mapper.Map<IEnumerable<RentalDTO>, List<RentalModel>>(orderService.GetRentals());
            return View(rentals);
        }
        public ActionResult More(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<RentalDTO, RentalModel>()).CreateMapper();
            var rental = mapper.Map<RentalDTO, RentalModel>(orderService.GetRentals().Where(r => r.Id == id).FirstOrDefault());
            ViewBag.Name = UserService.GetUsers().Where(u => u.Id == rental.ClientId).FirstOrDefault().Name;
            return View(rental);
        }
        [HttpPost]
        public ActionResult More(int id,string status,string message)
        {
            RentalDTO rental_new = orderService.GetRentals().Where(r => r.Id == id).FirstOrDefault();
            rental_new.Status = status;
            rental_new.Message = message;
            orderService.UpdateRental(rental_new);
            return RedirectToAction("Index");
        }


    }
}