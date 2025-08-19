using Flights.Server.Domain.Errors;
using Flights.Server.ReadModels;

namespace Flights.Server.Domain.Entities
{
    public record Flight(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        )
    {
        public IList<Booking> Bookings = new List<Booking>();

        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats; // Makes the property mutable

        public object? MakeBooking(string passengerEmail, byte numberOfSeats)
        {
            var flight = this; // 'this' refers to the current instance of the Flight record that we passed in

            // Domain validation rule
            if (flight.RemainingNumberOfSeats < numberOfSeats) // we check if there are enough seats available
                return new OverbookError();

            flight.Bookings.Add(            // we adjust the bookings list with the new booking
                new Booking(
                    passengerEmail,
                    numberOfSeats
                    )
                );

            flight.RemainingNumberOfSeats -= numberOfSeats;

            return null;
        }
    }
}
