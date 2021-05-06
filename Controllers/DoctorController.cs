
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
        [HttpGet]
        public IActionResult Index()
        {

            ViewBag.listing = DoctorModel.ShowDoctor();

            return View("../Doctor/Doctor");
        }

        // CustomerModelEntyties entyties
        [HttpPost]
        public IActionResult Create(DoctorModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, string title)
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
            string phone, string civilStatus, string title, int id)
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