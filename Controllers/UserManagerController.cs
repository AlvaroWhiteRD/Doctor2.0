
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class UserManagerController : Controller
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

                //si la session es distinta de 3(admin) no tendra acceso a esta area.
                if (HttpContext.Session.GetInt32("IdRol") != 3)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.listingDoctor = UserManagerModel.ShowUserDoctor();
                ViewBag.listingSecretary = UserManagerModel.ShowUserSecretary();

                return View("../UserManager/UserManager");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpGet]
        public IActionResult ShowUserDoctor()
        {
            ViewBag.listingDoctor = UserManagerModel.ShowUserDoctor();
            return View("../UserManager/UserManager");
        }
        // CustomerModelEntyties entyties
        [HttpPost]
        public int Create(UserManagerModelEntyties entyties,
            string userName, string password, int typeUserSelecte,
            string userSelect)
        {
            if (typeUserSelecte != 0)
            {
                //validamos el tipo de usuario
                //y se agregamos el rol segun el tipo de usuario
                switch (typeUserSelecte)
                {
                    case 1:
                        entyties.IdSecretary = typeUserSelecte;
                        entyties.IdRol = 1;
                        break;
                    case 2:
                        entyties.IdDoctor = typeUserSelecte;
                        entyties.IdRol = 2;
                        break;
                }
            }

            entyties.UserName = userName;
            entyties.Password = password;

            int myreturn = UserManagerModel.InsertNewUser(entyties);

            return myreturn;
        }
        [HttpPost]
        public int Update(UserManagerModelEntyties entyties,
            string userName,int typeUserSelecte, int id)
        {
            if (typeUserSelecte != 0)
            {
                //validamos el tipo de usuario
                //y se agregamos el rol segun el tipo de usuario
                switch (typeUserSelecte)
                {
                    case 1:
                        entyties.IdSecretary = typeUserSelecte;
                        entyties.IdRol = 1;
                        break;
                    case 2:
                        entyties.IdDoctor = typeUserSelecte;
                        entyties.IdRol = 2;
                        break;
                }
            }
            entyties.IdLogin = id;

            int myreturn = UserManagerModel.UpdateUser(entyties);
            return myreturn;

            //return Json(new { dataerror = true, errormsg = "This file format is not supported" });
        }

        [HttpDelete]
        public int Delete(int id)
        {
            int myreturn = UserManagerModel.DeleteUser(id);
            return myreturn;
        }

    }


}
