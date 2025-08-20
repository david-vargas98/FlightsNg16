using Flights.Server.Domain.Errors;
using Flights.Server.ReadModels;

namespace Flights.Server.Domain.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string Airline { get; set; }
        public string Price { get; set; }
        public TimePlace Departure { get; set; }
        public TimePlace Arrival { get; set; }
        public int RemainingNumberOfSeats { get; set; }

        public IList<Booking> Bookings = new List<Booking>();

        // Default constructor for creating an empty Flight object
        public Flight() { }
        
        // Constructor for creating a parametized Flight object
        public Flight(Guid id, string airline, string price, TimePlace departure, TimePlace arrival, int remainingNumberOfSeats)
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingNumberOfSeats;
        }

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
