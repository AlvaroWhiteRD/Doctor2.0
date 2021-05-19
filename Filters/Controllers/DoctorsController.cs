
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mr_Doctor.Models;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;

namespace Doctor2.Controllers
{
    public class DoctorsController : Controller
    {
        // 
        // GET: traemos todos los registros
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                //se llama la session
                ViewBag.SessionIdDoctor = HttpContext.Session.GetInt32("IdDoctor");
                ViewBag.SessionRol = HttpContext.Session.GetInt32("IdRol");

                //si la session es distinta de 1(secretaria) no tendra acceso a esta area.
                if (HttpContext.Session.GetInt32("IdRol") != 2)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.listing = AppointmentModel.ShowAppointmentForDoctor(ViewBag.SessionIdDoctor);

                return View("../Doctors/Doctor");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public int Update(DoctorsModelEntyties entyties,
                    string appointmentProcess,string prescribedMedical, 
                    string medicalNotes, int idDoctor, int idAppointment)
        {
            entyties.IdAppointment = idAppointment;
            entyties.IdDoctor = idDoctor;
            entyties.MedicalNotes = medicalNotes;
            entyties.PrescribedMedical = prescribedMedical;
            entyties.AppointmentProcess = appointmentProcess;


            int myreturn = DoctorsModel.UpdateAppointmentForDoctor(entyties);

            return myreturn;

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CustomerModel.DeleteCustomer(id);
            return View("../Customer/Customer");
        }

    }


}
