
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class SecretaryController : Controller
    {
        // 
        // GET: traemos todos los registros
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

                ViewBag.listing = SecretaryModel.ShowSecretary();

                return View("../Secretary/Secretary");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // CustomerModelEntyties entyties
        [HttpPost]
        public IActionResult Create(SecretaryModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus)
        {
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;
            //string dateNow= DateTime.Now.ToShortDateString("yyyy/mmmm/dddd");
            entyties.Date = DateTime.Now;


            int myreturn = SecretaryModel.InsertNewSecretary(entyties);
            //ViewBag.myreturn = myreturn;

            return View("../Secretary/Secretary");
        }
        [HttpPost]
        public IActionResult Update(SecretaryModelEntyties entyties,
            string name, string gender, string lastName,
            string motherLastname, string dni, string address,
            string phone, string civilStatus, int id)
        {
            entyties.IdSecretary = id;
            entyties.Name = name;
            entyties.LastName = lastName;
            entyties.MotherLastName = motherLastname;
            entyties.Dni = dni;
            entyties.Address = address;
            entyties.Phone = phone;
            entyties.CivilStatus = civilStatus;
            entyties.Gender = gender;

            int myreturn = SecretaryModel.UpdateSecretart(entyties);
            return View("../Secretary/Secretary");

            //return Json(new { dataerror = true, errormsg = "This file format is not supported" });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SecretaryModel.DeleteSecretary(id);
            return View("../Secretary/Secretary");
        }

        public List<SecretaryModelEntyties> Secretary()
        {
            List<SecretaryModelEntyties> list = new List<SecretaryModelEntyties>();
            list=SecretaryModel.ShowSecretary();
            return list;
        }

    }


}
