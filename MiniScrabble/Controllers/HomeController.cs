using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using MiniScrabble.Models;
using System.Diagnostics;
using System.Globalization;

namespace MiniScrabble.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
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

        private List<string> ReadSpecialWords()
        {
            var words = new List<string>();
            var filePath = Path.Combine(_env.WebRootPath, "csvData", "Words50.csv");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var word = csv.GetField<string>("Word");
                    if (!string.IsNullOrEmpty(word))
                    {
                        words.Add(word.Trim().ToUpper());
                    }
                }
            }
            return words;
        }

        [HttpGet]
        public IActionResult GetSpecialWords()
        {
            var specialWords = ReadSpecialWords();
            return Json(specialWords);
        }
    }
}