using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyApp // Note: actual namespace depends on the project name.
{
    class Comic
    {
        [JsonProperty("img")]
        public string IMG_link { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }
    }




    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Do you want the latest comic? (Y/N)");
                    var answer = Console.ReadLine();

                    if (string.IsNullOrEmpty(answer) || answer.ToLower()!="y")
                    {
                        break;
                    }


                    var result = await client.GetAsync("https://xkcd.com/info.0.json");
                    var resultRead = await result.Content.ReadAsStringAsync();
                    /*Console.WriteLine(resultRead);*/
                    var comic = JsonConvert.DeserializeObject<Comic>(resultRead);

                    Console.WriteLine("Title: " + comic.Title);
                    Console.WriteLine("Alt Text: " + comic.Alt);
                    Console.WriteLine("Image Link: " + comic.IMG_link);

                }
                catch (Exception)
                {
                    Console.WriteLine("Something inputted was wrong!");
                }

            }
        }


        /*        static void Main(string[] args)
                {

                }*/
    }
}