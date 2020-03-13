using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Site.BLL.DTO;
using Site.BLL.Infrastructure;
using Site.BLL.Interfaces;
using Site.WebUI.Models;
using Site.WebUI.Models.Identity;
using Site.WebUI.Models.Manager;
using Site.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Site.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        IOrderService orderService;
        public AdminController(IOrderService os)
        {
            orderService = os;
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CarAddModel carAdd, HttpPostedFileBase uploadImage, FormCollection form)
        {
            HttpPostedFileBase image = Request.Files["uploadImage"];
            if (uploadImage != null)
            {
                
                var mapper = new MapperConfiguration(c => c.CreateMap<CarAddModel, CarDTO>()).CreateMapper();
                var car = mapper.Map<CarAddModel, CarDTO>(carAdd);
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    car.Picture = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                car.PictureName = uploadImage.FileName;
                orderService.Add(car);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No image");
                return View();
            }
        }
        public ActionResult EditCar(int id)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, CarAddModel>()).CreateMapper();
            return View(mapper.Map<CarDTO, CarAddModel>(orderService.GetCars().Where(c => c.Id == id).FirstOrDefault()));
        }
        [HttpPost]
        public ActionResult EditCar(CarAddModel car)
        {
            var mapper = new MapperConfiguration(c => c.CreateMap<CarAddModel, CarDTO>()).CreateMapper();
            orderService.UpdateCar(mapper.Map<CarAddModel, CarDTO>(car));
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            IEnumerable<CarDTO> carDTO = orderService.GetCars();
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, CarViewModel>()).CreateMapper();
            var cars = mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carDTO);
            return View(cars);
        }
        public async Task<ActionResult> Delete(string email)
        {
            UserDTO user = UserService.GetUsers().Where(c => c.Email == email).FirstOrDefault();
            OperationDetails operationDetails = await UserService.Update(user);
            return RedirectToAction("Users");
        }
        public ActionResult Users()
        {
            UserStatusModel users = new UserStatusModel() { };
            var mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, UserModel>()).CreateMapper();
            users.UnlockedUsers= mapper.Map<IEnumerable<UserDTO>, List<UserModel>>(UserService.GetUsers().Where(u=>u.LockoutEnabled==false));
            users.BlockedUsers = mapper.Map<IEnumerable<UserDTO>, List<UserModel>>(UserService.GetUsers().Where(u => u.LockoutEnabled == true));
            return View(users);
        }
        public ActionResult DeleteCar(int id)
        {
            orderService.DeleteCar(id);
            return RedirectToAction("Index");
        }
        public ActionResult Update(string email)
        {
            UserDTO user = UserService.GetUsers().Where(u => u.Email == email).FirstOrDefault();
            if (user.LockoutEnabled == true)
                user.LockoutEnabled = false;
            else
                user.LockoutEnabled = true;
            UserService.Update(user);
            return RedirectToAction("Users");
        }
        public ActionResult AddManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddManager(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "manager"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succeded)
                    return View();
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View();
        }

    }
}