using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Server.DTO;
using Flights.Server.ReadModels;
using Flights.Server.Domain.Entities;
using Flights.Server.Data;

namespace Flights.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDTO dto)
        {
            _entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
                ));

            return CreatedAtAction(nameof(Find), new {email= dto.Email}, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            // This makes sure that values are matching and we're returning a PassengerRm and not a NewPassengerDTO
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);

            if (passenger == null)
                return NotFound();

            // Creating PassengerRm object using values from DTO
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
                );

            return Ok(rm);
        }
    }
}
