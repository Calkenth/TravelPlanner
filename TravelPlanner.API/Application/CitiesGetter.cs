using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.Data;

namespace TravelPlanner.API.Application
{

    public class CitiesGetter
    {
        private TravelPlannerContext _context;

        public CitiesGetter(TravelPlannerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetCities()
        {
            List<string> response = new List<string>();
            var cities = await _context.Cities.ToListAsync();

            if (cities.Count == 0)
            {
                throw new Exception("No cities found in database.");
            }

            foreach (var item in cities)
            {
                response.Add(item.Name);
            }

            return response;
        }
    }
}
