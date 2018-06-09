using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AddLocationExample
{
    public class JsonFileLocationRepository : ILocationRepository
    {

        public JsonFileLocationRepository(String jsonFile)
        {
            this.jsonFilePath = jsonFile;
            this.locations = new Lazy<List<Location>>(() => LoadFile(this.jsonFilePath));
        }


        private string jsonFilePath;
        private Lazy<List<Location>> locations;



        public List<Location> Locations
        {
            get => this.locations.Value.ToList();
        }


        public bool ContainsAddress(String formattedAddress)
        {
            return (this.locations.Value.Any(x => x.StanardizedAddress == formattedAddress));
        }


        public void AddLocation(Location location)
        {
            this.locations.Value.Add(location);
            this.SaveFile(this.jsonFilePath, this.locations.Value);
        }


        

        internal List<Location> LoadFile(String file)
        {
            if (!File.Exists(file))
                return new List<Location>();

            String content = File.ReadAllText(file);
            var locations = JsonConvert.DeserializeObject<List<Location>>(content);

            return locations;
        }



        internal void SaveFile(String filePath, List<Location> locations)
        {
            String json = JsonConvert.SerializeObject(locations);
            File.WriteAllText(filePath, json);
        }




    }
}
