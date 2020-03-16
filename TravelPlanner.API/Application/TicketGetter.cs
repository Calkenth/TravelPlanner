using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.Data;

namespace TravelPlanner.API.Application
{
    public class Ticket
    {
        public string FromWhere { get; set; }

        public string ToWhere { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }
    }

    public class BigTicket
    {
        public Guid Guid { get; set; }

        public double WholePrice { get; set; }

        public string FromWhere { get; set; }

        public string DepartureTime { get; set; }

        public string ToWhere { get; set; }

        public string ArrivalTime { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }

    public class TicketGetter
    {
        private readonly string hub = "linz";
        private readonly TravelPlannerContext _context;

        public TicketGetter(TravelPlannerContext context)
        {
            _context = context;
        }

        public async Task<List<BigTicket>> GetTickets(string fromCity, string toCity)
        {
            List<BigTicket> tickets = new List<BigTicket>();

            if (fromCity.ToLower() != hub && toCity.ToLower() != hub)
            {
                var toHub = await _context.FromTo.Include(t => t.Travels)
                                                 .Include(fC => fC.FromCity)
                                                 .Include(tC => tC.ToCity)
                                                 .Where(c => c.FromCity.Name == fromCity && c.ToCity.Name == hub)
                                                 .ToListAsync();

                var fromHub = await _context.FromTo.Include(t => t.Travels)
                                                   .Include(fC => fC.FromCity)
                                                   .Include(tC => tC.ToCity)
                                                   .Where(c => c.FromCity.Name == hub && c.ToCity.Name == toCity)
                                                   .ToListAsync();

                foreach (var item in toHub)
                {
                    foreach (var travel in item.Travels.OrderBy(x=>x.DepartureTime))
                    {
                        var first = new Ticket
                        {
                            FromWhere = item.FromCity.Name,
                            ToWhere = item.ToCity.Name,
                            DepartureTime = travel.DepartureTime.ToShortTimeString(),
                            ArrivalTime = travel.ArrivalTime.ToShortTimeString()
                        };
                        foreach(var secondItem in fromHub)
                        {
                            foreach(var secondTravel in secondItem.Travels.OrderBy(x => x.DepartureTime))
                            {
                                var second = new Ticket
                                {
                                    FromWhere = secondItem.FromCity.Name,
                                    ToWhere = secondItem.ToCity.Name,
                                    DepartureTime = secondTravel.DepartureTime.ToShortTimeString(),
                                    ArrivalTime = secondTravel.ArrivalTime.ToShortTimeString()
                                };
                                if(travel.ArrivalTime < secondTravel.DepartureTime)
                                {
                                    tickets.Add(new BigTicket
                                    {
                                        Guid = Guid.NewGuid(),
                                        WholePrice = travel.Price + secondTravel.Price,
                                        FromWhere = first.FromWhere,
                                        DepartureTime = first.DepartureTime,
                                        ToWhere = second.ToWhere,
                                        ArrivalTime = second.ArrivalTime,
                                        Tickets = new List<Ticket>
                                        {
                                            first,
                                            second
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var fromTo = await _context.FromTo.Include(t => t.Travels)
                                                  .Include(fC => fC.FromCity)
                                                  .Include(tC => tC.ToCity)
                                                  .Where(c => c.FromCity.Name == fromCity && c.ToCity.Name == toCity)
                                                  .ToListAsync();

                tickets = Remodel(tickets, fromTo);
            }

            if(tickets.Count == 0)
            {
                throw new Exception("No tickets found - no routes found in databse.");
            }

            return tickets;
        }

        private List<BigTicket> Remodel(List<BigTicket> responseList, List<DomainModels.FromTo> itemList)
        {
            foreach (var item in itemList)
            {
                foreach (var travel in item.Travels.OrderBy(x => x.DepartureTime))
                {
                    responseList.Add(new BigTicket
                    {
                        Guid = Guid.NewGuid(),
                        WholePrice = item.Travels.First().Price,
                        FromWhere = item.FromCity.Name,
                        DepartureTime = travel.DepartureTime.ToShortTimeString(),
                        ToWhere = item.ToCity.Name,
                        ArrivalTime = travel.ArrivalTime.ToShortTimeString(),
                        Tickets = new List<Ticket>
                        {
                            new Ticket
                            {
                                FromWhere = item.FromCity.Name,
                                ToWhere = item.ToCity.Name,
                                DepartureTime = travel.DepartureTime.ToShortTimeString(),
                                ArrivalTime = travel.ArrivalTime.ToShortTimeString()
                            }
                        }
                    });
                }
            }

            return responseList;
        }
    }
}
