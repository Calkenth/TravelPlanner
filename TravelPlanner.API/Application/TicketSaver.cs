using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.Data;

namespace TravelPlanner.API.Application
{
    public class Order
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string FromWhere { get; set; }

        public string DepartureTime { get; set; }

        public string ToWhere { get; set; }

        public string ArrivalTime { get; set; }

        public int NumberOfPersons { get; set; }
    }

    public class TicketSaver
    {
        private readonly TravelPlannerContext _context;

        public TicketSaver(TravelPlannerContext context)
        {
            _context = context;
        }

        public async Task<Order> SaveTicket(BigTicket ticket, string Name, int NumberOfPersons)
        {
            Order response;
            var first = ticket.Tickets.First();
            var last = ticket.Tickets.Last();

            try
            {
                Order order = new Order
                {
                    Name = Name,
                    ArrivalTime = last.ArrivalTime,
                    DepartureTime = first.DepartureTime,
                    FromWhere = ticket.FromWhere,
                    ToWhere = ticket.ToWhere,
                    NumberOfPersons = NumberOfPersons,
                    Price = ticket.WholePrice
                };

                response = await SaveTicketToDB(order);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        private async Task<Order> SaveTicketToDB(Order order)
        {
            var existingTicket = await _context.OrderedTickets.FirstOrDefaultAsync(o => o.Name == order.Name
                                                                                        && o.FromWhere == order.FromWhere
                                                                                        && o.ToWhere == order.ToWhere
                                                                                        && o.Timestamp == DateTime.Now);

            if (existingTicket != null)
            {
                throw new Exception("This ticket is already ordered.");
            }

            await _context.OrderedTickets.AddAsync(new DomainModels.Order
            {
                Timestamp = DateTime.Now,
                Name = order.Name,
                Price = Math.Round(order.Price * order.NumberOfPersons,2),
                FromWhere = order.FromWhere,
                DepartureTime = order.DepartureTime,
                ToWhere = order.ToWhere,
                ArrivalTime = order.ArrivalTime
            });

            var success = await _context.SaveChangesAsync();
            if (success != 1)
            {
                throw new Exception("No changes in database were done.");
            }

            return order;
        }
    }
}
