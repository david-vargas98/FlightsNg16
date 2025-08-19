using System.ComponentModel.DataAnnotations;

namespace Flights.Server.Domain.Entities
{
    public record Booking(
        Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats
        );
}
