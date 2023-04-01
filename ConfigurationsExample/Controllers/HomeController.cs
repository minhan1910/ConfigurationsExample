using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationsExample.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IConfiguration _configuration;

        private readonly WeatherApiOptions _options;
        public HomeController(IOptions<WeatherApiOptions> weatherApiOptions)
        {
            _options = weatherApiOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            // Cách 1:
            //ViewBag.ClientID = _configuration["weatherapi:ClientID"]; 
            //ViewBag.ClientSecret = _configuration.GetValue<string>("weatherapi:ClientSecret", "the default client secret");

            // Cách 2: optimize 1 way
            //IConfigurationSection weatherapiSection = _configuration.GetSection("weatherapi");
            // ViewBag.ClientID = weatherapiSection["ClientID"];
            // ViewBag.ClientSecret = weatherapiSection["ClientSecret"];

            // Cách 3: Dùng Options pattern
            // Get<>() -> auto bind
            //WeatherApiOptions options = _configuration
            //                                .GetSection("weatherapi")
            //                                .Get<WeatherApiOptions>();
            // ViewBag.ClientID = options.ClientID;
            // ViewBag.ClientSecret = options.ClientSecret;

            // Bind: Load configuration values into existing Options object
            //WeatherApiOptions options = new WeatherApiOptions();
            //_configuration.GetSection("weatherapi").Bind(options);
            //ViewBag.ClientID = options.ClientID;
            //ViewBag.ClientSecret = options.ClientSecret;
            
            // Cách 4: dùng DI để dùng thì sẽ clean hơn khi ko cần viết nhiều code ở đây
            ViewBag.ClientID = _options.ClientID;
            ViewBag.ClientSecret = _options.ClientSecret;

            return View();
        }
    }
}
