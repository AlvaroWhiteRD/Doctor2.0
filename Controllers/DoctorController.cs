
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    
    public class DoctorController : Controller
    {
        // 
        // GET: traemos todos los registros
        //[Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                ViewBag.listing = DoctorModel.ShowDoctor();
                ViewBag.User = HttpContext.Session.GetString("User");
                ViewBag.User = HttpContext.Session.GetString("User");
                //return View("../Doctor/Doctor");
                //return RedirectToAction("Index", "Doctor");
                return View();
            }
            else {
                ViewBag.Error = HttpContext.Session.GetString("User");
                return View("../Login/Login");
            }
            
        }

        // CustomerModelEntyties entyties
        [HttpPost]
        public IActionResult Create(DoctorModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, string title, 
            string execuatur,string specialty)
        {
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;
            entyties.Title = title;
            entyties.Execuatur = execuatur;
            entyties.Specialty = specialty;
            //string dateNow= DateTime.Now.ToShortDateString("yyyy/mmmm/dddd");
            entyties.Date = DateTime.Now;


            int myreturn = DoctorModel.InsertNewDoctor(entyties);
            //ViewBag.myreturn = myreturn;

            return View("../Doctor/Doctor");

        }
        [HttpPost]
        public IActionResult Update(DoctorModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, string title, string execuatur, string specialty, int id)
        {
            entyties.IdDoctor = id;
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;
            entyties.Title = title;
            entyties.Execuatur = execuatur;
            entyties.Specialty = specialty;


            int myreturn = DoctorModel.UpdateDoctor(entyties);
            return View("../Doctor/Doctor");

            //return Json(new { dataerror = true, errormsg = "This file format is not supported" });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            DoctorModel.DeleteDoctor(id);
            return View("../Doctor/Doctor");
        }

    }


}