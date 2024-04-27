using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Flights_Serve.Domain.Models
{
    public class Flights
    {
        public int Id { get; set; } = 0;

        [MaxLength(50, ErrorMessage = "The field has exceeded the character limit")]
        [MinLength(1, ErrorMessage = "The field does not meet the minimum number of characters")]
        public string Origin { get; set; } = null;

        [MaxLength(50, ErrorMessage = "The field has exceeded the character limit")]
        [MinLength(1, ErrorMessage = "The field does not meet the minimum number of characters")]
        public string Destination { get; set; } = null;

        [Column(TypeName = "decimal (18,2)")]
        [DisplayFormat(DataFormatString = "0:C2")]
        public decimal price { get; set; }

        [MaxLength(50, ErrorMessage = "The field has exceeded the character limit")]
        [MinLength(1, ErrorMessage = "The field does not meet the minimum number of characters")]
        public string FlightCarrier { get; set; } = null;

        [Column(TypeName = "decimal (18,2)")]
        [DisplayFormat(DataFormatString = "0:C2")]
        public decimal FlightNumber { get; set; }
    }
}
