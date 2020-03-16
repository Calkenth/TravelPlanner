using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TravelPlanner.API.Application;
using TravelPlanner.API.Data;
using TravelPlanner.API.Models;

namespace TravelPlanner.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TravelPlannerController : ControllerBase
    {
        readonly CitiesGetter citiesGetter;
        readonly RouteGetter routeGetter;
        readonly TicketGetter ticketGetter;
        readonly TicketSaver ticketSaver;
        readonly OrderedTicketsGetter orderedTicketsGetter;
        private IEnumerable<BigTicket> actuallTickets;
        private string actuallTicketsAsJson;
        private readonly string ticketsSession = "_Tickets";

        public TravelPlannerController(TravelPlannerContext context)
        {
            citiesGetter = new CitiesGetter(context);
            routeGetter = new RouteGetter(context);
            ticketGetter = new TicketGetter(context);
            ticketSaver = new TicketSaver(context);
            orderedTicketsGetter = new OrderedTicketsGetter(context);
        }

        [HttpGet("{delay:int=0}")]
        public async Task<ActionResult> GetCities(int delay)
        {
            await Task.Delay(delay);
            try
            {
                var response = await citiesGetter.GetCities();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{delay:int=0}")]
        public async Task<ActionResult> GetRoutes(int delay)
        {
            await Task.Delay(delay);
            try
            {
                var response = await routeGetter.GetAllRoutes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{fromCity}/{toCity}/{delay:int=0}")]
        public async Task<ActionResult> GetRoutes(string fromCity, string toCity, int delay)
        {
            await Task.Delay(delay);
            try
            {
                var cities = await citiesGetter.GetCities();
                if (!cities.Any(x => x.ToLower() == fromCity.ToLower()))
                    return NotFound($"{fromCity} doesn't exist in database.");
                if (!cities.Any(x => x.ToLower() == toCity.ToLower()))
                    return NotFound($"{toCity} doesn't exist in database.");

                var response = await routeGetter.GetRoute(fromCity, toCity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{fromCity}/{toCity}/{delay:int=0}")]
        public async Task<ActionResult> GetTickets(string fromCity, string toCity, int delay)
        {
            await Task.Delay(delay);
            try
            {
                var cities = await citiesGetter.GetCities();

                if (!cities.Any(x => x.ToLower() == fromCity.ToLower()))
                    return NotFound($"{fromCity} doesn't exist in database.");
                if (!cities.Any(x => x.ToLower() == toCity.ToLower()))
                    return NotFound($"{toCity} doesn't exist in database.");

                var response = await ticketGetter.GetTickets(fromCity, toCity);

                actuallTicketsAsJson = JsonConvert.SerializeObject(response);
                HttpContext.Session.SetString(ticketsSession, actuallTicketsAsJson);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{delay:int=0}")]
        public async Task<ActionResult> OrderTicket(int delay,[FromBody] OrderRequest orderRequest)
        {
            await Task.Delay(delay);
            try
            {
                actuallTicketsAsJson = HttpContext.Session.GetString(ticketsSession);
                if (string.IsNullOrWhiteSpace(actuallTicketsAsJson))
                    throw new Exception("No tickets stored in session.");
                actuallTickets = JsonConvert.DeserializeObject<IEnumerable<BigTicket>>(actuallTicketsAsJson);

                var ticket = actuallTickets.Single(t => t.Guid == orderRequest.Guid);

                var response = await ticketSaver.SaveTicket(ticket, orderRequest.Name, orderRequest.NumberOfPersons);

                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Ticket guid doesn't exist.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{delay:int=0}")]
        public async Task<ActionResult> GetOrderedTickets(int delay)
        {
            await Task.Delay(delay);
            try
            {
                var response = await orderedTicketsGetter.GetOrderedTickets();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}