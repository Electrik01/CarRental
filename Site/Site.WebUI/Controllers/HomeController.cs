using AutoMapper;
using Site.BLL.BusinessModels;
using Site.BLL.DTO;
using Site.BLL.Interfaces;
using Site.WebUI.Models.Manager;
using Site.WebUI.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IOrderService orderService;
        public HomeController(IOrderService os)
        {
            orderService = os;
        }

        public ActionResult Index()
        {
            HomeModel model = new HomeModel()
            {
                Marks = orderService.GetMarks().Select(m => m.Name).ToList(),
                CarTypes = orderService.GetCarTypes().Select(ct => ct.Name).ToList()
            };

            return View(model);          
        }  


        public ActionResult SortType(string sortOrder,string carType="",string mark="",string search="")
        {

            var carDTO = orderService.GetCars();
            if (carType != "")
                carDTO = orderService.GetCars().Where(c =>carType.Contains(c.CarTypeS));
            if (mark != "")
                carDTO = carDTO.Where(c => mark.Contains(c.MarkS));
            if (search != "")
            {
                search = search.ToUpper();
                carDTO = carDTO.Where(c => c.Name.Contains(search));
            }
            switch (sortOrder)
            {
                case "Name (descending)":
                    carDTO = carDTO.OrderByDescending(c => c.Name);  
                    break;
                case "Price (ascending)":
                    carDTO = carDTO.OrderBy(c => c.Price);
                    break;
                case "Price (descending)":
                    carDTO = carDTO.OrderByDescending(c => c.Price);
                    break;
                case "Name (ascending)":
                    carDTO = carDTO.OrderBy(c => c.Name);
                    break;
            }
                
            var mapper = new MapperConfiguration(c => c.CreateMap<CarDTO, CarViewModel>()).CreateMapper();
            var cars = mapper.Map<IEnumerable<CarDTO>, List<CarViewModel>>(carDTO);
            return PartialView("Cars",cars.ToList());
        }
    }
}