﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.Server.DTO;
using Flights.Server.ReadModels;

namespace Flights.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        static private IList<NewPassengerDTO> Passengers = new List<NewPassengerDTO>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDTO dto)
        {
            Passengers.Add(dto);
            System.Diagnostics.Debug.WriteLine(Passengers.Count); // Debug on console :0
            return CreatedAtAction(nameof(Find), new {email= dto.Email}, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            // This makes sure that values are matching and we're returning a PassengerRm and not a NewPassengerDTO
            var passenger = Passengers.FirstOrDefault(p => p.Email == email);

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
