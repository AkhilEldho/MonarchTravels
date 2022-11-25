using Client.Models;
using Client.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Test URL : https://localhost:7161/
        public IActionResult Index()
        {
            GetFlightName();
            GetTourDestination();
            GetAccomodation();

            return View();
        }

        public IActionResult Checkout(string flight, string accomodation, string tour)
        {
            Flight bookedFlight = searchFlight(flight);
            Accomodation bookedAccomodation = searchAccomodation(accomodation);
            Tour bookedTour = searchTour(tour);

            string message = "";
            decimal total = 0;

            if (bookedAccomodation.AccomodationAddress != bookedTour.TourDestination)
                message = "Choose same destination locations";
            else if (bookedAccomodation.AccomodationAddress == bookedTour.TourDestination)
                total = bookedFlight.FlightCost + bookedAccomodation.AccomodationCost + bookedTour.TourCost;


            ViewBag.Flight = "Flight Name : " + bookedFlight.FlightName + "<br/>Flight Cost : $" + Math.Round(bookedFlight.FlightCost, 2); ;

            ViewBag.Accomodation = "Accomodation Name : " + bookedAccomodation.AccomodationName + "<br/>Location : " + bookedAccomodation.AccomodationAddress + "<br/>Type : " + bookedAccomodation.AccomodationType + "<br/>Accomodation Cost : $" + Math.Round(bookedAccomodation.AccomodationCost, 2);

            ViewBag.Tour = "Tour Destination: " + bookedTour.TourDestination + "<br/>Tour Cost : $" + Math.Round(bookedTour.TourCost,2);

            ViewBag.Total = Math.Round(total,2);

            return View();
        }

        //getting name of all flights
        public List<Flight> GetFlightName()
        {
            string products = "";
            List<Flight> names = new List<Flight>();

            try
            {
                //establish variables needed for connecting to azure
                string URL = "http://localhost:7000/api/FlightDetail";

                //HTTP Client
                HttpClient Client = new HttpClient();
                HttpResponseMessage responseMessage = Client.GetAsync(URL).Result;
                HttpContent httpContent = responseMessage.Content;

                if (responseMessage != null)
                    products = httpContent.ReadAsStringAsync().Result;
                else
                    products = "Error";

                names = JsonConvert.DeserializeObject<List<Flight>>(products);
            }
            catch (Exception ex)
            {
                names = null;
            }

            ViewBag.flightNames = new SelectList(names, "Flights");

            return names;
        }

        //search for flight
        public Flight searchFlight(string name)
        {
            Flight found = null;
            List<Flight> flights = GetFlightName();

            foreach (Flight flight in flights)
            {
                if (flight.FlightName == name)
                    found = flight;
            }

            return found;
        }

        //getting name of all tour destinations
        public List<Tour> GetTourDestination()
        {
            string products = "";
            List<Tour> destinations = new List<Tour>();

            try
            {
                //establish variables needed for connecting to azure
                string URL = "http://localhost:7000/api/TourDetail";

                //HTTP Client
                HttpClient Client = new HttpClient();
                HttpResponseMessage responseMessage = Client.GetAsync(URL).Result;
                HttpContent httpContent = responseMessage.Content;

                if (responseMessage != null)
                    products = httpContent.ReadAsStringAsync().Result;
                else
                    products = "Error";

                destinations = JsonConvert.DeserializeObject<List<Tour>>(products);
            }
            catch (Exception ex)
            {
                destinations = null;
            }

            ViewBag.tourDestinations = new SelectList(destinations, "Tour");

            return destinations;
        }

        //searching for tour
        public Tour searchTour(string name)
        {
            Tour found = null;
            List<Tour> tours = GetTourDestination();

            foreach (Tour tour in tours)
            {
                if (tour.TourDestination == name)
                    found = tour;
            }

            return found;
        }

        //getting all list of accomodations
        public List<Accomodation> GetAccomodation()
        {
            string products = "";
            List<Accomodation> accomodations = new List<Accomodation>();

            try
            {
                //establish variables needed for connecting to azure
                string URL = "http://localhost:7000/api/AccomodoationDetail";

                //HTTP Client
                HttpClient Client = new HttpClient();
                HttpResponseMessage responseMessage = Client.GetAsync(URL).Result;
                HttpContent httpContent = responseMessage.Content;

                if (responseMessage != null)
                    products = httpContent.ReadAsStringAsync().Result;
                else
                    products = "Error";

                accomodations = JsonConvert.DeserializeObject<List<Accomodation>>(products);
            }
            catch (Exception ex)
            {
                accomodations = null;
            }

            ViewBag.accomodationNames = new SelectList(accomodations, "Accomodation");

            return accomodations;
        }

        //Searching for accomodation object
        public Accomodation searchAccomodation(string name)
        {
            Accomodation found = null;
            List<Accomodation> accomodations = GetAccomodation();

            foreach (Accomodation accomodation in accomodations)
            {
                if (accomodation.AccomodationAddress == name)
                    found = accomodation;
            }

            return found;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}