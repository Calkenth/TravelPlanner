using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TravelPlanner.API.DomainModels
{
    public class FromTo
    {
        public int FromToID { get; set; }

        [Column("From_CityID")]
        public City FromCity { get; set; }

        [Column("To_CityID")]
        public City ToCity { get; set; }

        public ICollection<Travel> Travels { get; set; }
    }
}