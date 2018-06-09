using System;
using System.Collections.Generic;
using System.Text;

namespace AddLocationExample
{
    public interface IGeocodeService
    {

        GeocodeResult GeocodeAddress(String streetAddress, String city, String state);

    }
}
