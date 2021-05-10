
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

            ViewBag.listing = AppointmentModel.ShowAppointment();

            return View("../Appointment/Appointment");
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

    }


}