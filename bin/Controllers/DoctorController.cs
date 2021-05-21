
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

        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                //se llama la session
                ViewBag.SessionUser = HttpContext.Session.GetString("User");
                ViewBag.SessionRol = HttpContext.Session.GetInt32("IdRol");

                //si la session es distinta de 1(secretaria) no tendra acceso a esta area.
                if (HttpContext.Session.GetInt32("IdRol") != 1)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.listing = DoctorModel.ShowDoctor();

                return View("../Doctor/Doctor");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }

        // CustomerModelEntyties entyties
        [HttpPost]
        public IActionResult Create(DoctorModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, string title,
            string execuatur, string specialty)
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

        public List<DoctorModelEntyties> Doctor()
        {
            List<DoctorModelEntyties> list = new List<DoctorModelEntyties>();
            list = DoctorModel.ShowDoctor();
            return list;
        }

    }


}
