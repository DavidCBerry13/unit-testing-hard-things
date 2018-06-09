using CommandLine;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace AddLocationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleCommandLineParseErrors(errs));

            Console.ReadKey();
        }


        static void RunOptionsAndReturnExitCode(Options options)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();


            IGeocodeService geocodeService = new GoogleGeocoder(configuration);
            ILocationRepository locationRepository = new JsonFileLocationRepository("locations.json");

            LocationService service = new LocationService(geocodeService, locationRepository);
            try
            {
                var location = service.AddLocation(options.Name, options.StreetAddress, options.City, options.State);
                Console.WriteLine($"Address for {location.Name} added at {location.StanardizedAddress}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void HandleCommandLineParseErrors(IEnumerable<Error> errors)
        {

        }



    }
}
