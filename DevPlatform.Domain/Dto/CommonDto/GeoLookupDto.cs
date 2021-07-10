namespace DevPlatform.Domain.Dto.CommonDto
{
    public class GeoLookupDto
    {
        public string CurrentCountryName { get; set; }
        public string CurrentCityName { get; set; }
        public string CurrentCountryIsoCode { get; set; }
        public string CurrentCityIsoCode { get; set; } 
        public string CurrentPostalCode { get; set; }
        public string CurrentContinentName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public bool? IsInEuropeanUnion { get; set; } 
    }
}
