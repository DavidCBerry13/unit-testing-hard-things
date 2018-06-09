using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddLocationExample
{
    class Options
    {

        [Option('n', "name", Required = true, HelpText = "Location Name.")]
        public String Name { get; set; }

        [Option('a', "streetAddress", Required = true, HelpText = "Street Address.")]
        public String StreetAddress { get; set; }

        [Option('c', "city", Required = true, HelpText = "City.")]
        public String City { get; set; }

        [Option('s', "state", Required = true, HelpText = "State.")]
        public String State { get; set; }
    }
}
