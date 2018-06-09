using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AddLocationExample
{
    public class GoogleGeocoder : IGeocodeService
    {

        public GoogleGeocoder(IConfiguration config)
        {
            this.Configuration = config;
        }


        public IConfiguration Configuration { get; set; }


        public GeocodeResult GeocodeAddress(String streetAddress, String city, String state)
        {
            var apiKey = Configuration["googleGecodeKey"];

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json");
            var addressString = WebUtility.UrlEncode($"{streetAddress} {city} {state}");

            var response =  httpClient.GetAsync($"?address={addressString}&key={apiKey}");
            Task.WaitAll(response);

            Task<GoogleGeocodeResponse> responseContent = response.Result.Content.ReadAsAsync<GoogleGeocodeResponse>();
            
            return this.MapResponse(responseContent.Result);
        }



        public GeocodeResult MapResponse(GoogleGeocodeResponse googleResponse)
        {
            if (googleResponse.Results.Length == 0)
                return null;

            var bestMatch = googleResponse.Results[0];
            GeocodeResult result = new GeocodeResult()
            {
                StreetAddress = $"{this.ExtractValue(bestMatch, "street_number")} {this.ExtractValue(bestMatch, "route")}",
                City = this.ExtractValue(bestMatch, "locality"),
                State = this.ExtractValue(bestMatch, "administrative_area_level_1"),
                ZipCode = this.ExtractValue(bestMatch, "postal_code"),
                FormattedAddress = bestMatch.FormattedAddress,               
            };

            return result;
        }


        public String ExtractValue(Result result, String typeKey)
        {
            var addressComponent = result.AddressComponents                  
                    .FirstOrDefault(c => c.Types.Any(t => t == typeKey));

            return (addressComponent != null) ? addressComponent.ShortName : String.Empty;
        }




    }
}
