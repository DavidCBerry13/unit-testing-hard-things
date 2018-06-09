using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddLocationExample
{


        public partial class GoogleGeocodeResponse
        {
            [JsonProperty("results")]
            public Result[] Results { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }
        }

        public partial class Result
        {
            [JsonProperty("address_components")]
            public AddressComponent[] AddressComponents { get; set; }

            [JsonProperty("formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }

            [JsonProperty("place_id")]
            public string PlaceId { get; set; }

            [JsonProperty("types")]
            public string[] Types { get; set; }
        }

        public partial class AddressComponent
        {
            [JsonProperty("long_name")]
            public string LongName { get; set; }

            [JsonProperty("short_name")]
            public string ShortName { get; set; }

            [JsonProperty("types")]
            public string[] Types { get; set; }
        }

        public partial class Geometry
        {
            [JsonProperty("location")]
            public Geolocation Location { get; set; }

            [JsonProperty("location_type")]
            public string LocationType { get; set; }

            [JsonProperty("viewport")]
            public Viewport Viewport { get; set; }
        }

        public partial class Geolocation
        {
            [JsonProperty("lat")]
            public double Latitude { get; set; }

            [JsonProperty("lng")]
            public double Longitude { get; set; }
        }

        public partial class Viewport
        {
            [JsonProperty("northeast")]
            public Location Northeast { get; set; }

            [JsonProperty("southwest")]
            public Location Southwest { get; set; }
        }


    
}
