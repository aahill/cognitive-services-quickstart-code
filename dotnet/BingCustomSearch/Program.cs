// <usingDirectives>
using System;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Search.CustomSearch;
// </usingDirectives>

namespace bing_custom
{
    class Program
    {

        // <authentication>
        static CustomSearchClient authenticate(string key)
        {
            var searchClient = new CustomSearchClient(new ApiKeyServiceClientCredentials(key));
            return searchClient;
        }
        // </authentication>
        // <sendRequest>
        static void sendRequest(CustomSearchClient searchClient, string query, string customConfig)
        {   
            var webData = searchClient.CustomInstance.SearchAsync(query: query, customConfig: customConfig).Result;
        
            if (webData?.WebPages?.Value?.Count > 0)
            {
                // find the first web page
                var firstWebPagesResult = webData.WebPages.Value.FirstOrDefault();

                if (firstWebPagesResult != null)
                {
                    Console.WriteLine("Number of webpage results {0}", webData.WebPages.Value.Count);
                    Console.WriteLine("First web page name: {0} ", firstWebPagesResult.Name);
                    Console.WriteLine("First web page URL: {0} ", firstWebPagesResult.Url);
                }
                else
                {
                    Console.WriteLine("No web results were found.");
                }
            }
            else
            {
                Console.WriteLine("No web data received...");
            }
        }
        // <main>
        static void Main(string[] args)
        {
            string searchTerm = "Xbox";
            string customConfig = "YOUR-CUSTOM-CONFIG-ID"; //you can also use "1"
            string key = Environment.GetEnvironmentVariable("BING_CUSTOM_SEARCH_SUBSCRIPTION_KEY");
            var client = authenticate(key);
            sendRequest(client, searchTerm, customConfig);
        }
        // </main>
    }
}
