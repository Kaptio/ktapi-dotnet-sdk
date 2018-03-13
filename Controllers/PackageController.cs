using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Client2.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Client2.Utils;
using Client2.Services;

namespace Client2.Controllers
{
    [Route("api/[controller]")]
    public class PackageController : Controller
    {
        private readonly PackageContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private MyAppData _appState;

        public PackageController(PackageContext context, ILoggerFactory logger, MyAppData appState, IConfiguration configuration)
        {
            _context = context;
            _appState = appState;
            _configuration = configuration;
            _logger = logger.CreateLogger("Client2.Controllers.PackageController");

        }

        [HttpGet]
        public IActionResult GetAllPackages()
        {
            var output = new Dictionary<string, object>();
            var packages = PackageService.GetPackages(null, null, _context, _logger, _appState, _configuration);
            output.Add("packages", packages);
            output.Add("total", _context.Package.Count());
            return new ObjectResult(output);
        }


        [HttpGet("page/{page:int}/{size:int}")]
        public IActionResult GetPackagesWithPagination(int page, int size)
        {
            var output = new Dictionary<string, object>();
            try
            {
                var packages = PackageService.GetPackages(page, size, _context, _logger, _appState, _configuration);
                output.Add("packages", packages);
                output.Add("total", _context.Package.Count());
                return new ObjectResult(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.GetBaseException());
                output.Add("exception", ex.Message);
                return StatusCode(500, output);
            }
        }


        [HttpGet("{id}", Name = "GetPackage")]
        public IActionResult GetById(string id)
        {
            var output = new Dictionary<string, object>();
            var package = PackageService.GetPackageWithDetails(id, _context, _configuration);
            if (package == null)
            {
                output.Add("exception", "Package not found.");
                return NotFound(output);
            }
            output.Add("package", package);
            return new ObjectResult(output);
        }

        [HttpPost("{id}/prices", Name = "GetPackagePaces")]
        public IActionResult GetPackegePrices(string id, [FromBody] object data)
        {

            Console.WriteLine(data);

            var output = new Dictionary<string, object>();
            var prices = PackageService.GetPackagePrices(id, data, _context, _configuration);
            output.Add("prices", prices);
            return new ObjectResult(output);
        }

        [HttpPost("{id}/prices_with_alternatives", Name = "GetPackagePacesWithAlternatives")]
        public IActionResult GetPackegePricesWithAlternatives(string id, [FromBody] object data)
        {

            Console.WriteLine(data);

            var output = new Dictionary<string, object>();
            var result = PackageService.GetPackagePricesWithAlternatives(id, data, _context, _configuration);
            output.Add("data", result);
            return new ObjectResult(output);
        }

    }

}