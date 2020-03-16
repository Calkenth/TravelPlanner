using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TravelPlanner.API.DomainModels
{
    public class City
    {
        public int CityID { get; set; }

        [JsonProperty("City")]
        public string Name { get; set; }
    }
}