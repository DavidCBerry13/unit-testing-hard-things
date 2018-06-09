using System;
using System.Collections.Generic;
using System.Text;

namespace AddLocationExample
{
    public interface ILocationRepository
    {


        List<Location> Locations { get; }

        bool ContainsAddress(String standardizedAddress);

        void AddLocation(Location location);

    }
}
