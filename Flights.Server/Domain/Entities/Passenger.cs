using System.ComponentModel.DataAnnotations;

namespace Flights.Server.Domain.Entities;

public record Passenger(
    string Email,
    string FirstName,
    string LastName,
    bool Gender
    );
