using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ODataOnPremise.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class WeatherForecastController : ODataController //ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        [EnableQuery]
        public IEnumerable<WeatherForecast> Get()
        {
            //Request.Host = new Microsoft.AspNetCore.Http.HostString("onpremiseserver.org");
            //Request.PathBase = string.Empty;

            //var coties = Enumerable.Range(1, 5).Select(index => new City
            //{
            //    Code = new Random().Next(1, 6),
            //    Name = "city"
            //}).ToList();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Key = new Random().Next(1, 6),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                //Cities = coties
            })
            .ToArray();
        }


        //!/WeatherForecast(key)
        //[EnableQuery]
        //public ActionResult<WeatherForecast> Get([FromODataUri] int key)
        //{
        //    Request.Host = new Microsoft.AspNetCore.Http.HostString("onpremiseserver.org");
        //    Request.PathBase = string.Empty;

        //    var rng = new Random();

        //    var coties = Enumerable.Range(1, 5).Select(index => new City
        //    {
        //        Code = new Random().Next(1, 6),
        //        Name = "city"
        //    }).ToList();
            
        //    var obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        key = new Random().Next(1, 6),
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)],
        //        Cities = coties

        //    }).FirstOrDefault(o => o.key == key);

        //    return obj;
        //}

        ////~/WeatherForecast(key)/Cities
        //[EnableQuery]
        //public ActionResult<List<City>> GetCities(/*[FromODataUri] int key*/)
        //{
        //    //Request.Host = new Microsoft.AspNetCore.Http.HostString("onpremiseserver.org");
        //    //Request.PathBase = string.Empty;

        //    var rng = new Random();

        //    var coties = Enumerable.Range(1, 5).Select(index => new City
        //    {
        //        Code = new Random().Next(1, 6),
        //        Name = "city"
        //    }).ToList();


        //    var obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        key = new Random().Next(1, 6),
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)],
        //        Cities = coties

        //    }).FirstOrDefault(o => o.key == 1);

        //    return obj.Cities;
        //}



        //[ODataRoute("Default.OverideGrades")]
        //[HttpGet]
        //public ActionResult<List<Grade>> OverideGrades(/*[FromODataUri] int key*/)
        //{
        //    //Request.Host = new Microsoft.AspNetCore.Http.HostString("onpremiseserver.org");
        //    //Request.PathBase = string.Empty;

        //    var rng = new Random();

        //    var coties = Enumerable.Range(1, 5).Select(index => new Grade
        //    {
        //        key = new Random().Next(1, 6),
        //        grade = new Random().Next(1, 6)
        //    }).ToList();

        //    return coties;

        //    //var obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    //{
        //    //    key = new Random().Next(1, 6),
        //    //    Date = DateTime.Now.AddDays(index),
        //    //    TemperatureC = rng.Next(-20, 55),
        //    //    Summary = Summaries[rng.Next(Summaries.Length)],
        //    //    Cities = coties

        //    //}).FirstOrDefault(o => o.key == 1);


        //}


    }
}
