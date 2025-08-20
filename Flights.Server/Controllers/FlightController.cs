using Flights.Server.ReadModels;
using Microsoft.AspNetCore.Mvc;
using Flights.Server.DTO;
using Flights.Server.Domain.Entities;
using Flights.Server.Domain.Errors;
using Flights.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Flights.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly Entities _entities;

        public FlightController(ILogger<FlightController> logger, Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }

        [HttpGet]
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(500)] // internal server error
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]

        public IEnumerable<FlightRm> Search([FromQuery] FlightSearchParametersDTO @params)
        {
            var flightRmList = _entities.Flights.Select(flight => new FlightRm( // we convert the flights to a read model for sending to the client
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                )
            ).ToArray();

            return flightRmList;
        }

        [ProducesResponseType(404)] // 404 instead of StatusCodes.Status404NotFound for not found
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]
        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var readModel = new FlightRm( // we convert the flight to a read model for sending to the client
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                );
            
            return Ok(readModel);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDTO dto)
        {
            System.Diagnostics.Debug.WriteLine($"Bookin' a new flight {dto.FlightId}");

            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
                return NotFound();

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
                return Conflict(new { message= "Not enough seats available for booking" });

            try
            {
                _entities.SaveChanges();
            } catch (DbUpdateConcurrencyException e)
            {
                return Conflict(new { message = "An error ocurred while booking, try again." });
            }

            return CreatedAtAction(nameof(Find), new {id= dto.FlightId}, dto);
        }

    }
}
