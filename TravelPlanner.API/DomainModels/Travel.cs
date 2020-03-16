using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TravelPlanner.API.DomainModels
{
    public class Travel
    {
        public int TravelID { get; set; }
        public int FromToID { get; set; }

        [DataType(DataType.Time)]
        [JsonProperty("Leave")]
        [Column("Departure_Time")]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Time)]
        [JsonProperty("Arrive")]
        [Column("Arrival_Time")]
        public DateTime ArrivalTime { get; set; }

        [DataType(DataType.Currency)]
        [JsonProperty("Price")]
        public double Price { get; set; }
    }
}