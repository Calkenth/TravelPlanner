using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.API.Models
{
    public class OrderRequest
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfPersons { get; set; }
    }
}
