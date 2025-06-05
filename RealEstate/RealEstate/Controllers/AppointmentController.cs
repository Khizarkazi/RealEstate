using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Services;
using RealEstate.RepoDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly RealEstateContext _db;
        private readonly IPropertiesRepo _repo;

        public AppointmentController(RealEstateContext db, IPropertiesRepo repo)
        {
            _db = db;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            ViewBag.PropertyList = new SelectList(_db.Properties, "PropertyId", "Title");
            return View(new Appointment());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAppointment(Appointment appointment)
        {
            ViewBag.PropertyList = new SelectList(_db.Properties, "PropertyId", "Title");

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.UserId = 1; // TODO: Replace with logged in user id
                    appointment.Status = "Pending";

                    _repo.AddAppointment(appointment);

                    ViewBag.Message = "Appointment added successfully!";
                    ModelState.Clear();
                    return View(new Appointment());
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error while saving appointment: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill all required fields correctly.");
            }

            return View(appointment);
        }

        public IActionResult Fetch()
        {
            var appointments = _repo.GetAllAppointments();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult UpdateRemark(int id, string remark)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == id);

            if (appointment != null)
            {
                appointment.AgentRemark = remark;
                appointment.Status = remark; // Update status same as remark
                _db.SaveChanges();
            }

            return RedirectToAction("Fetch");
        }
    }
}
