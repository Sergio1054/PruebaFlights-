using Azure.Core;
using Flights_Serve.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights_Serve.Aplication
{
    public class FlighService : IFlightService, IFlightServiceCreated, IFlightserviceUpd, IFlightserviceDeleated, IFlightserviceFile
    {
        private readonly FlightsContexts _context;
        public readonly IWebHostEnvironment _hostingEnvironment;


        public FlighService(FlightsContexts context)
        {
            _context = context;
        }

        public async Task<string> CreateFlightAsync(Flights flights)
        {
            try {

                await _context.Flights.AddAsync(flights);
                await _context.SaveChangesAsync();
                return "record created successfully";

            }catch (Exception ex)
            {
                // Capturar la excepción interna y devolver su mensaje
                string innerErrorMessage = ex.InnerException != null ? ex.InnerException.Message : "";
                return $"Error al insertar: {ex.Message}. Detalles: {innerErrorMessage}";
            }
        }

        public async Task<List<object>> GetFilteredFlightsAsync(string Origin = null, string Destination = null, decimal? Price = null, string Type = null, string FlightCarrier = null, string FlightNumber = null)
        {
            var query = _context.Flights.AsQueryable();


            if (!string.IsNullOrEmpty(Origin))
            {
                query = query.Where(f => f.Origin == Origin);
            }

            if (!string.IsNullOrEmpty(Destination))
            {
                query = query.Where(f => f.Destination == Destination);
            }

            if (Price.HasValue)
            {
                query = query.Where(f => f.Price >= Price.Value);
            }

            if (!string.IsNullOrEmpty(Type))
            {
                query = query.Where(f => f.Type == Type);
            }

            if (!string.IsNullOrEmpty(FlightCarrier))
            {
                query = query.Where(f => f.FlightCarrier == FlightCarrier);
            }

            if (!string.IsNullOrEmpty(FlightNumber))
            {
                decimal flightNumberDecimal;
                if (decimal.TryParse(FlightNumber, out flightNumberDecimal))
                {
                    query = query.Where(f => f.FlightNumber == flightNumberDecimal);
                }
            }

            var flights = await query.ToListAsync();

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
            try
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

            }catch (Exception ex)
            {
                return $"Error al actualizar: {ex.Message}";
            }
        }

        public async Task<string> DeleteFlightAsync(int id)
        {
            try {
                var existingFlight = await _context.Flights.FindAsync(id);
                if (existingFlight == null)
                    return "Flight not found";

                _context.Flights.Remove(existingFlight);
                await _context.SaveChangesAsync();

                return "record deleted successfully";
            } catch (Exception ex) {
                return $"Error al eliminar: {ex.Message}";
            }
        }

        public async Task<string> FileFlightAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "No se proporcionó ningún archivo.";
            }

            try
            {
                // Leer el contenido del archivo
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    // Guardar el archivo en la ubicación deseada
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Infrastructure", "JsonDataBase", fileName);
                    await File.WriteAllBytesAsync(filePath, fileBytes);

                    // Devolver información sobre el archivo subido
                    return $"Archivo '{fileName}' subido exitosamente.";
                }
            }
            catch (Exception ex)
            {
                return $"Error al subir el archivo: {ex.Message}";
            }
        }

    }
}
