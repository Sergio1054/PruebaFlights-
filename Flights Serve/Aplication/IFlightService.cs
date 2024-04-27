namespace Flights_Serve.Aplication
{
    public interface IFlightService
    {
          Task<List<object>> GetFlightsAsync();
    }
}
