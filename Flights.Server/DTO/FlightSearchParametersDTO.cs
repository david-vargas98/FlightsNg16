using System.ComponentModel;

namespace Flights.Server.DTO
{
    public record FlightSearchParametersDTO(
        [DefaultValue("08/20/2025 10:30:00 AM")]
        DateTime? FromDate,

        [DefaultValue("08/21/2025 10:30:00 AM")]
        DateTime? ToDate,

        [DefaultValue("Los angeles")]
        string? From,

        [DefaultValue("Berlin")]
        string? Destination,

        [DefaultValue(1)]
        int? NumberOfPassengers
        );
}
