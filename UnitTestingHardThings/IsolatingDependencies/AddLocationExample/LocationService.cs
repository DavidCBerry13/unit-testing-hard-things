using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddLocationExample
{
    public class LocationService
    {

        public LocationService(IGeocodeService geocodeService, ILocationRepository locationRepository)
        {
            this._geocoderService = geocodeService;
            this._locationRepository = locationRepository;
        }


        private IGeocodeService _geocoderService;
        private ILocationRepository _locationRepository;




        public Location AddLocation(String name, String streetAddress, String city, String state)
        {

            var geocoderResult = this._geocoderService.GeocodeAddress(streetAddress, city, state);

            if ( this._locationRepository.ContainsAddress(geocoderResult.FormattedAddress) )
                throw new ApplicationException("This address already exists");


            Location location = new Location()
            {
                Name = name,
                StreetAddress = geocoderResult.StreetAddress,
                City = geocoderResult.City,
                State = geocoderResult.State,
                ZipCode = geocoderResult.ZipCode,
                StanardizedAddress = geocoderResult.FormattedAddress
            };
            this._locationRepository.AddLocation(location);

            return location;
        }




    }
}
