using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace PasswordSafeConsole.Jokes
{
    internal class JokeLoader
    {
        private const string UriString = "https://api.chucknorris.io/jokes/random";

        public static string RequestJoke()
        {
            string responseString = GetJokeResponse();
            return ExtractJoke(responseString);
        }

        private static string ExtractJoke(string responseString)
        {
            Match result = Regex.Match(responseString, ".*\\\"value\\\":\\\"(.*)\\\"\\}");
            string jokeOnly = result.Groups[1].Value;
            return jokeOnly;
        }

        private static string GetJokeResponse()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response =
                client.Send(new HttpRequestMessage()
                {
                    RequestUri = new Uri(UriString),
                    Method = HttpMethod.Get
                });
            string responseString = response.Content.ReadAsStringAsync().Result;
            return responseString;
        }
    }
}