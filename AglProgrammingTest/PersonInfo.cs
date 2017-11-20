using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace AGLProgrammingTest
{
    public class PersonList
    {
        public PersonList()
        {
            Persons = new List<PersonInfo>();
        }             
        
        public List<PersonInfo> Persons { get; set; }

    }

    public class PersonInfo
    {
        public PersonInfo()
        {
            Pets = new List<Pet>();
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("pets")]
        public List<Pet> Pets { get; set; }

    }

    public class Pet
    {
        [JsonProperty("name")]
        public string PetName { get; set; }
        [JsonProperty("type")]
        public string PetType { get; set; }
    }
}
