namespace DevPlatform.Domain.Dto.CommonDto
{
    public class WeatherResponseDto
    {
        public string CurrentCityName { get; set; }
        public string CurrentCountryName { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }
        public string WeatherIcon { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherMain { get; set; }
    }
}
