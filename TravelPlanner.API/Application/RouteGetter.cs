using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.Data;

namespace TravelPlanner.API.Application
{
    public class Route
    {
        public double Price { get; set; }

        public string FromWhere { get; set; }

        public string ToWhere { get; set; }

        public ICollection<string> DepartureTime { get; set; }

        public ICollection<string> ArrivalTime { get; set; }
    }

    public class RouteGetter
    {
        private readonly string hub = "Linz";
        private TravelPlannerContext _context;

        public RouteGetter(TravelPlannerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Route>> GetAllRoutes()
        {
            List<Route> routes = new List<Route>();
            var fromTo = await _context.FromTo.Include(t => t.Travels)
                                              .Include(fC => fC.FromCity)
                                              .Include(tC => tC.ToCity)
                                              .ToListAsync();

            if(fromTo.Count == 0)
            {
                throw new Exception("No routes found in database.");
            }

            routes = Remodel(routes, fromTo);

            return routes;
        }

        public async Task<IEnumerable<Route>> GetRoute(string fromCity, string toCity)
        {
            List<Route> routes = new List<Route>();

            if (fromCity != hub && toCity != hub)
            {
                var toHub = await _context.FromTo.Include(t => t.Travels)
                                                 .Include(fC => fC.FromCity)
                                                 .Include(tC => tC.ToCity)
                                                 .Where(c => c.FromCity.Name == fromCity && c.ToCity.Name == hub)
                                                 .ToListAsync();

                routes = Remodel(routes, toHub);
                var fromHub = await _context.FromTo.Include(t => t.Travels)
                                                   .Include(fC => fC.FromCity)
                                                   .Include(tC => tC.ToCity)
                                                   .Where(c => c.FromCity.Name == hub && c.ToCity.Name == toCity)
                                                   .ToListAsync();

                routes = Remodel(routes, fromHub);
            }
            else
            {
                var fromTo = await _context.FromTo.Include(t => t.Travels)
                                                  .Include(fC => fC.FromCity)
                                                  .Include(tC => tC.ToCity)
                                                  .Where(c => c.FromCity.Name == fromCity && c.ToCity.Name == toCity)
                                                  .ToListAsync();

                routes = Remodel(routes, fromTo);
            }

            if(routes.Count == 0)
            {
                throw new Exception("No routes found in database.");
            }

            return routes;
        }

        private List<Route> Remodel(List<Route> responseList,List<DomainModels.FromTo> itemList)
        {
            foreach (var item in itemList)
            {
                List<string> depart = new List<string>();
                List<string> arrive = new List<string>();

                foreach (var travel in item.Travels.OrderBy(x => x.DepartureTime))
                {
                    depart.Add(travel.DepartureTime.ToShortTimeString());
                    arrive.Add(travel.ArrivalTime.ToShortTimeString());
                }
                responseList.Add(new Route
                {
                    Price = item.Travels.First().Price,
                    FromWhere = item.FromCity.Name,
                    ToWhere = item.ToCity.Name,
                    DepartureTime = depart,
                    ArrivalTime = arrive
                });
            }

            return responseList;
        }
    }
}
