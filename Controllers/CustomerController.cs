using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mr_Doctor.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CustomerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[HttpPost]
        public IActionResult Customer(CustomerModelEntyties entyties,string name, string lastName, string motherLastname,string dni,string address, string phone, string civilStatus,string gender, bool ars, DateTime date)
        {
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;
            entyties.Ars = ars;
            entyties.Date = date;

            //CustomerModel.InsertNewCustomer(entyties);

            return View("../Customer/Customers");
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

