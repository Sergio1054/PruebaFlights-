using Flights_Serve.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flights_Serve.Aplication
{
    public interface IFlightService
    {
        Task<List<object>> GetFilteredFlightsAsync(string Origin = null, string Destination = null, decimal? Price = null, string Type = null, string FlightCarrier = null, string FlightNumber = null);
    }

    public interface IFlightServiceCreated
    {
        Task<string> CreateFlightAsync(Flights flights);
    }

    public interface IFlightserviceUpd
    {
        Task<string> UpdateFlightAsync(int id, Flights flights);
    }
    public interface IFlightserviceDeleated
    {
        Task<string> DeleteFlightAsync(int id);
    }

}
