
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

            ViewBag.listing = SecretaryModel.ShowSecretary();

            return View("../Secretary/Secretary");
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