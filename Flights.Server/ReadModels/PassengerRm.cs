namespace Flights.Server.ReadModels
{
    public record PassengerRm(
        string Email,
        string FirstName,
        string LastName,
        bool Gender
        );
}
