using Flights_Serve.Aplication;
using Flights_Serve.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Flights_Serve.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsContexts _context;
        private readonly IFlightService _flightService;

        public FlightsController(FlightsContexts context, IFlightService flightService)
        {
            _context = context;
            _flightService = flightService;
        }

        //[HttpPost]
        //[Route("Created")]
        //public async Task<IActionResult> CreatedFlight(Flights flights)
        //{
        //    var result = await _flightService.CreateFlightAsync(flights);
        //    return Ok(result);
        //}

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> ListRegisters()
        {
            var result = await _flightService.GetFlightsAsync();
            return Ok(result);
        }

    //    [HttpPut]
    //    [Route("Updated/{id}")]
    //    public async Task<ActionResult> UpdateFlights(int id, Flights flights)
    //    {
    //        var result = await _flightService.UpdateFlightAsync(id, flights);
    //        return Ok(result);
    //    }

    //    [HttpDelete]
    //    [Route("Delete/{id}")]
    //    public async Task<ActionResult> DeleteFlights(int id)
    //    {
    //        var result = await _flightService.DeleteFlightAsync(id);
    //        return Ok(result);
    //    }
    }
}
