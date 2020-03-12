namespace ODataOnPremise
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class WeatherForecast
    {
        [Key]
        public int Key { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        //public List<City> Cities{ get; set; }
        //public List<Grade> Grades{ get; set; }
    }


    //public class Grade
    //{
    //    [Key]
    //    public int key { get; set; }
    //    public int grade { get; set; }
    //}
}
