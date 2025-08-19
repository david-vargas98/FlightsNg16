namespace Flights.Server.DTO
{
    public record BookDTO(
        Guid FlightId,
        string PassengerEmail,
        byte NumberOfSeats
        );
}
