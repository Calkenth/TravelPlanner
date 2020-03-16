using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.API.DomainModels
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime Timestamp { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string FromWhere { get; set; }

        public string DepartureTime { get; set; }

        public string ToWhere { get; set; }

        public string ArrivalTime { get; set; }
    }
}
