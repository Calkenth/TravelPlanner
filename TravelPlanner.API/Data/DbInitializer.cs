using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanner.API.DomainModels;

namespace TravelPlanner.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TravelPlannerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Travels.Any())
            {
                return;
            }

            var Linz = new City { Name = "Linz" };
            var Vienna = new City { Name = "Vienna" };
            var Salzburg = new City { Name = "Salzburg" };
            var Graz = new City { Name = "Graz" };

            context.Cities.Add(Linz);
            context.Cities.Add(Vienna);
            context.Cities.Add(Salzburg);
            context.Cities.Add(Graz);
            context.SaveChanges();


            var fromto = new FromTo { FromCity = Salzburg, ToCity = Linz };

            context.FromTo.Add(fromto);
            context.SaveChanges();

            double price = 10.5;

            var travel = new Travel { FromToID = fromto.FromToID, DepartureTime = DateTime.Parse("09:00"), ArrivalTime = DateTime.Parse("10:45"), Price = price };
            var travel1 = new Travel { FromToID = fromto.FromToID, DepartureTime = DateTime.Parse("11:00"), ArrivalTime = DateTime.Parse("12:15"), Price = price };
            var travel2 = new Travel { FromToID = fromto.FromToID, DepartureTime = DateTime.Parse("13:00"), ArrivalTime = DateTime.Parse("14:15"), Price = price };
            var travel3 = new Travel { FromToID = fromto.FromToID, DepartureTime = DateTime.Parse("15:00"), ArrivalTime = DateTime.Parse("16:45"), Price = price };

            context.Travels.Add(travel);
            context.Travels.Add(travel1);
            context.Travels.Add(travel2);
            context.Travels.Add(travel3);
            context.SaveChanges();

            var fromto1 = new FromTo { FromCity = Linz, ToCity = Salzburg };

            context.FromTo.Add(fromto1);
            context.SaveChanges();


            var travel4 = new Travel { FromToID = fromto1.FromToID, DepartureTime = DateTime.Parse("11:00"), ArrivalTime = DateTime.Parse("12:15"), Price = price };
            var travel5 = new Travel { FromToID = fromto1.FromToID, DepartureTime = DateTime.Parse("12:30"), ArrivalTime = DateTime.Parse("13:45"), Price = price };
            var travel6 = new Travel { FromToID = fromto1.FromToID, DepartureTime = DateTime.Parse("14:30"), ArrivalTime = DateTime.Parse("16:00"), Price = price };
            var travel7 = new Travel { FromToID = fromto1.FromToID, DepartureTime = DateTime.Parse("17:00"), ArrivalTime = DateTime.Parse("18:45"), Price = price };

            context.Travels.Add(travel4);
            context.Travels.Add(travel5);
            context.Travels.Add(travel6);
            context.Travels.Add(travel7);
            context.SaveChanges();

            var fromto2 = new FromTo { FromCity = Vienna, ToCity = Linz };

            context.FromTo.Add(fromto2);
            context.SaveChanges();

            price = 30.20;

            var travel8 = new Travel { FromToID = fromto2.FromToID, DepartureTime = DateTime.Parse("08:00"), ArrivalTime = DateTime.Parse("10:30"), Price = price };
            var travel9 = new Travel { FromToID = fromto2.FromToID, DepartureTime = DateTime.Parse("11:00"), ArrivalTime = DateTime.Parse("13:15"), Price = price };
            var travel10 = new Travel { FromToID = fromto2.FromToID, DepartureTime = DateTime.Parse("14:00"), ArrivalTime = DateTime.Parse("16:30"), Price = price };
            var travel11 = new Travel { FromToID = fromto2.FromToID, DepartureTime = DateTime.Parse("17:00"), ArrivalTime = DateTime.Parse("19:15"), Price = price };

            context.Travels.Add(travel8);
            context.Travels.Add(travel9);
            context.Travels.Add(travel10);
            context.Travels.Add(travel11);
            context.SaveChanges();

            var fromto3 = new FromTo { FromCity = Linz, ToCity = Vienna };

            context.FromTo.Add(fromto3);
            context.SaveChanges();

            var travel12 = new Travel { FromToID = fromto3.FromToID, DepartureTime = DateTime.Parse("11:00"), ArrivalTime = DateTime.Parse("13:15"), Price = price };
            var travel13 = new Travel { FromToID = fromto3.FromToID, DepartureTime = DateTime.Parse("13:30"), ArrivalTime = DateTime.Parse("15:45"), Price = price };
            var travel14 = new Travel { FromToID = fromto3.FromToID, DepartureTime = DateTime.Parse("16:45"), ArrivalTime = DateTime.Parse("19:00"), Price = price };
            var travel15 = new Travel { FromToID = fromto3.FromToID, DepartureTime = DateTime.Parse("19:30"), ArrivalTime = DateTime.Parse("21:45"), Price = price };

            context.Travels.Add(travel12);
            context.Travels.Add(travel13);
            context.Travels.Add(travel14);
            context.Travels.Add(travel15);
            context.SaveChanges();

            var fromto4 = new FromTo { FromCity = Graz, ToCity = Linz };

            context.FromTo.Add(fromto4);
            context.SaveChanges();

            price = 25;

            var travel16 = new Travel { FromToID = fromto4.FromToID, DepartureTime = DateTime.Parse("06:00"), ArrivalTime = DateTime.Parse("09:00"), Price = price };
            var travel17 = new Travel { FromToID = fromto4.FromToID, DepartureTime = DateTime.Parse("12:00"), ArrivalTime = DateTime.Parse("15:00"), Price = price };
            var travel18 = new Travel { FromToID = fromto4.FromToID, DepartureTime = DateTime.Parse("16:00"), ArrivalTime = DateTime.Parse("19:00"), Price = price };

            context.Travels.Add(travel16);
            context.Travels.Add(travel17);
            context.Travels.Add(travel18);
            context.SaveChanges();

            var fromto5 = new FromTo { FromCity = Linz, ToCity = Graz };

            context.FromTo.Add(fromto5);
            context.SaveChanges();

            var travel19 = new Travel { FromToID = fromto5.FromToID, DepartureTime = DateTime.Parse("10:00"), ArrivalTime = DateTime.Parse("13:00"), Price = price };
            var travel20 = new Travel { FromToID = fromto5.FromToID, DepartureTime = DateTime.Parse("15:30"), ArrivalTime = DateTime.Parse("18:30"), Price = price };
            var travel21 = new Travel { FromToID = fromto5.FromToID, DepartureTime = DateTime.Parse("19:45"), ArrivalTime = DateTime.Parse("22:30"), Price = price };

            context.Travels.Add(travel19);
            context.Travels.Add(travel20);
            context.Travels.Add(travel21);
            context.SaveChanges();        
        }
    }
}
