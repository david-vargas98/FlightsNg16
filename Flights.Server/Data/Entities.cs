using Flights.Server.Domain.Entities;
using System;

namespace Flights.Server.Data
{
    public class Entities
    {
        public IList<Passenger> Passengers = new List<Passenger>();
        public List<Flight> Flights = new List<Flight>();
    }
}
