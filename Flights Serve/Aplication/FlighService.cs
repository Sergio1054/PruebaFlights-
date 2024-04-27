using Flights_Serve.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights_Serve.Aplication
{
    public class FlighService : IFlightService, IFlightServiceCreated, IFlightserviceUpd, IFlightserviceDeleated
    {
        private readonly FlightsContexts _context;

        public FlighService(FlightsContexts context)
        {
            _context = context;
        }

        public async Task<string> CreateFlightAsync(Flights flights)
        {
            await _context.Flights.AddAsync(flights);
            await _context.SaveChangesAsync();
            return "record created successfully";
        }

        public async Task<List<object>> GetFlightsAsync()
        {
            var flights = await _context.Flights.ToListAsync();
            return flights.Select(f => new
            {
                f.Id,
                f.Origin,
                f.Destination,
                f.Price,
                f.Type,
                Transport = new
                {
                    f.FlightCarrier,
                    f.FlightNumber
                }
            }).ToList<object>();
        }

        public async Task<string> UpdateFlightAsync(int id, Flights flights)
        {
            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight == null)
                return "Flight not found";

            existingFlight.Origin = flights.Origin;
            existingFlight.Destination = flights.Destination;
            existingFlight.Price = flights.Price;
            existingFlight.Type = flights.Type;
            existingFlight.FlightCarrier = flights.FlightCarrier;
            existingFlight.FlightNumber = flights.FlightNumber;

            await _context.SaveChangesAsync();

            return "record updated successfully";
        }

        public async Task<string> DeleteFlightAsync(int id)
        {
            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight == null)
                return "Flight not found";

            _context.Flights.Remove(existingFlight);
            await _context.SaveChangesAsync();

            return "record deleted successfully";
        }
    }
}