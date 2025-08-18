namespace Flights.Server.DTO
{
    public record NewPassengerDTO(
        string Email,
        string FirstName,
        string LastName,
        bool Gender
        );
}
