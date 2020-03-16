using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.Data;

namespace TravelPlanner.API.Application
{
    public class OrderedTicket
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string FromWhere { get; set; }

        public string DepartureTime { get; set; }

        public string ToWhere { get; set; }

        public string ArrivalTime { get; set; }
    }

    public class OrderedTicketsGetter
    {
        private readonly TravelPlannerContext _context;
        public OrderedTicketsGetter(TravelPlannerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderedTicket>> GetOrderedTickets()
        {
            List<OrderedTicket> orderedTickets = new List<OrderedTicket>();
            var tickets = await _context.OrderedTickets.OrderByDescending(x => x.Timestamp).ToListAsync();

            if(tickets.Count == 0)
            {
                throw new Exception("No bought tickets in database.");
            }

            foreach(var ticket in tickets)
            {
                orderedTickets.Add(new OrderedTicket
                {
                    Name = ticket.Name,
                    Price = ticket.Price,
                    FromWhere = ticket.FromWhere,
                    DepartureTime = ticket.DepartureTime,
                    ToWhere = ticket.ToWhere,
                    ArrivalTime = ticket.ArrivalTime
                });
            }

            return orderedTickets;
        }
    }
}
