
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class CustomerController : Controller
    {
        // 
        // GET: traemos todos los registros
        [HttpGet]
        public IActionResult Index()
        {

            ViewBag.listing = CustomerModel.ShowCustomer();

            return View("../Customer/Customer");
        }

      
        [HttpPost]
        public IActionResult Create(CustomerModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, int ars)
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
            //string dateNow= DateTime.Now.ToShortDateString("yyyy/mmmm/dddd");
            entyties.Date = DateTime.Now;


            int myreturn = CustomerModel.InsertNewCustomer(entyties);
            //ViewBag.myreturn = myreturn;

            return View("../Customer/Customer");

        }
        [HttpPost]
        public IActionResult Update(CustomerModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, int ars, int id)
        {
            entyties.IdCustomer = id;
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;


            int myreturn = CustomerModel.UpdateCustomer(entyties);
            return View("../Customer/Customer");

            //return Json(new { dataerror = true, errormsg = "This file format is not supported" });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CustomerModel.DeleteCustomer(id);
            return View("../Customer/Customer");
        }
        /*[HttpGet]
        public IActionResult EditCustomer(int id)
        {
            //Array listing;
            List<CustomerModelEntyties> listing =  new List<CustomerModelEntyties>();
            listing = CustomerModel.EditCustomer(id);
            //return listing;
            return View("../Customer/Customer", listing);
        }*/
        //[HttpGet]
        //metodo que trae los datos para ser se mostrados por js en la vista
        //public ActionResult<IEnumerable<CustomerModelEntyties>> GetCustomer() =>
        // CustomerModel.ShowCustomer();

    } 
    

}