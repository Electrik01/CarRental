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
    public class ProfileController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        IOrderService orderService;
        public ProfileController(IOrderService os)
        {
            orderService = os;
        }
        public ActionResult Index(string id)
        {

            if (id != null && UserService.GetUsers().Where(c => c.Id == id).FirstOrDefault() != null)
            {
                UserDTO userDTO = UserService.GetUsers().Where(c => c.Id == id).FirstOrDefault();
                var mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, ProfileModel>()).CreateMapper();
                var profile = mapper.Map<UserDTO, ProfileModel>(userDTO);
                mapper = new MapperConfiguration(c => c.CreateMap<RentalDTO, RentalModel>()).CreateMapper();
                profile.Rental = mapper.Map<RentalDTO, RentalModel>(orderService.GetRentals().Where(r => r.ClientId == id).LastOrDefault());
                return View(profile);
            }
            return HttpNotFound();
        }
    }
}