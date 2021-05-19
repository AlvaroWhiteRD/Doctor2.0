
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class AppointmentController : Controller
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

                ViewBag.listing = AppointmentModel.ShowAppointment();

                return View("../Appointment/Appointment");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           

            
        }

        // CustomerModelEntyties entyties
        [HttpPost]
        public int Create(AppointmentModelEntyties entyties,
            int idDoctor, int idCustomer, string timeAppointment,
            string description, DateTime dateAppointment)
        {
            entyties.IdDoctor = idDoctor;
            entyties.IdCustomer = idCustomer;
            entyties.TimeAppointment = timeAppointment;
            entyties.Description = description;
            entyties.State = "pendiente";
            entyties.DateAppointment = dateAppointment;
            entyties.AppointmentCreated = DateTime.Now;

            int myreturn;
            try
            {

                myreturn = AppointmentModel.InsertNewAppointment(entyties);

            }
            catch (Exception)
            {
                myreturn = -1;

            }
           return myreturn;
            //ViewBag.myreturn = myreturn;

            //return View("../Appointment/Appointment");

        }
        [HttpPost]
        public int Update(AppointmentModelEntyties entyties, int idAppointment,
            int idDoctor, int idCustomer, string timeAppointment,
            string description, string state, DateTime dateAppointment)
        {
            entyties.IdAppointment = idAppointment;
            entyties.IdDoctor = idDoctor;
            entyties.IdCustomer = idCustomer;
            entyties.TimeAppointment = timeAppointment;
            entyties.Description = description;
            entyties.State = state;
            entyties.DateAppointment = dateAppointment;


            int myreturn;
            try
            {
                myreturn = AppointmentModel.UpdateAppointment(entyties);

            }
            catch (Exception)
            {

                myreturn = -1;
            }

            return myreturn;

            //return Json(new { dataerror = true, errormsg = "This file format is not supported" });
        }

        [HttpDelete]
        public int Delete(int id)
        {
            int myreturn;
            try
            {
                myreturn = AppointmentModel.DeleteAppointment(id);
            }
            catch (Exception)
            {

                myreturn = -1;
            }
            return myreturn;
            //return View("../Doctor/Doctor");
        }

        
        [HttpGet]
        public List<AppointmentModelEntyties> GetDataCalendar()
        {//metodo que trae la data para luego mostrarla en el calendario usando ajax

            return AppointmentModel.ShowAppointment();

            //return View("../Appointment/Appointment");
        }

    }


}