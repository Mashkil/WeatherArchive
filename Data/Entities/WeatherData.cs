using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Entities
{
    public class WeatherData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public double TemperatureC { get; set; }

        [Required]
        public double AirHumidity { get; set; }

        [Required]
        public double DewPoint { get; set; }

        public int PressureMmHg { get; set; }

        public string? WindDirection { get; set; }

        public int? WindSpeed { get; set; }

        public int? Cloudy { get; set; }

        public int? CloudCover { get; set; }

        public int? HorizontalVisibility { get; set; }

        public string? WeatherEvents { get; set; }
    }
}
