using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace AGLProgrammingTest
{
    class Program
    {
        private static string ServiceUrl
        {
            get
            {
                return ConfigurationSettings.AppSettings["ServiceUrl"];
            }
        }
        static void Main(string[] args)
        {
           List<PersonInfo> persons = GetRequest().Result;

            var personsHavingCats = from person in persons
                       where person.Pets != null
                       group person.Pets by person.Gender into g
                       select new { g.Key, pets = from a in g.ToList()
                                                  from b in a.ToList()
                                                  where b.PetType.Equals("Cat")
                                                  orderby b.PetName select b };
           
            personsHavingCats.ToList().ForEach(x => {
                Console.WriteLine(x.Key);
                x.pets.ToList().ForEach(y =>
                {
                    Console.WriteLine("   " + y.PetName);
                });
            });
                      

            Console.ReadKey();
        }

        private static async Task<List<PersonInfo>> GetRequest()
        {          
            using (HttpClient client = new HttpClient())
            {
                //service url  
                Uri url = new Uri(ServiceUrl);
                //clear request headers
                client.DefaultRequestHeaders.Clear();
                //Define request data format - json  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ///Async get request call 
                HttpResponseMessage response = await client.GetAsync(url);
                var jsonStr = response.Content.ReadAsStringAsync().Result;
                //deserialize json string object
                return JsonConvert.DeserializeObject<List<PersonInfo>>(jsonStr);
            }
        }
    }
}
