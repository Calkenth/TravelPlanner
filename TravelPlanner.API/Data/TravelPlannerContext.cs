using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.DomainModels;

namespace TravelPlanner.API.Data
{
    public class TravelPlannerContext : DbContext
    {
        public TravelPlannerContext(DbContextOptions options)
            : base(options)
        {
        }

        protected TravelPlannerContext()
        {
        }

        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<FromTo> FromTo { get; set; }
        public virtual DbSet<Order> OrderedTickets { get; set; }
    }
}
