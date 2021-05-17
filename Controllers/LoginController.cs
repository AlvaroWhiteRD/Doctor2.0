
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class LoginController : Controller
    {
        // 
        // GET: traemos todos los registros
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModelEntyties user)
        {

            //List<LoginModelEntyties> login = LoginModel.Login(user);
            List<LoginModelEntyties> login = LoginModel.Login(user);

            //si la session existe no se deja pasar al login
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Index", "Home");
            }


            if (login.Count < 1)
            {
                ViewBag.Error = "Error: usuario o contraseña incorrecto";
                return View();
            }
            ViewBag.SessionUser = "";
            if (login.Count > 0)
            {
                foreach (var item in login)
                {
                    //si el usuario y contraseña coinciden se crea la session
                    HttpContext.Session.SetString("User", item.UserName);
                    HttpContext.Session.SetInt32("IdRol", item.IdRol);

                }
                //se llama la session
                ViewBag.SessionUser = HttpContext.Session.GetString("User");
                ViewBag.SessionRol = HttpContext.Session.GetInt32("IdRol");
            }
            return RedirectToAction("Index", "Home");


            /* if (login.Count > 0)
             {

                      HttpContext.Session.SetString("user", Convert.ToString(user.UserName));
                        HttpContext.Session.SetInt32("rol", user.IdLogin);

                     ViewBag.sessionU = HttpContext.Session.GetString("user");
                     ViewBag.sessionR = HttpContext.Session.GetInt32("rol");
                     TempData["msg"] = "Bienvenido " + user.IdRol;


             }
             else
             {
                 TempData["msg"] = "Error: usuario o contraseña incorrecto";
             }*/

            //return View();
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

    }


}