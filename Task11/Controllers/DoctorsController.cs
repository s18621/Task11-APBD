using Microsoft.AspNetCore.Mvc;
using Task11.Models;
using Task11.Services;

namespace Task11.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbServiece service;
        public DoctorsController(IDbServiece service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(service.getDoctors());
        }
        [HttpPut]
        public IActionResult AddDoctor(Doctor doctor)
        {
            service.putDoctor(doctor);
            return Ok("Dodano doktora");
        }
        [HttpPut("edit")]
        public IActionResult EditDoctor(Doctor doctor)
        {
            return Ok(service.editDoctor(doctor));
        }


        [HttpDelete("delete")]
        public IActionResult DeleteDoctor(Doctor doctor)
        {
            var result = service.delDoctor(doctor);
            if (result != null) return Ok("Doktor usuniety");

            return BadRequest("Nie ma takiego doktora");
        }
    }
}
