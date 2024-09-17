using Models;
using Models.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.Design.Serialization;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Services
{
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;

        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<GitHubEvent> GetRecentGithubEventsByUserName(string userName)
        {
            //_httpClient.BaseAddress = new Uri(string.Format("https:////api.github.com//users//{0}//events", userName));
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.37.3");
            var response = _httpClient.GetAsync(string.Format("https://api.github.com/users/{0}/events", userName)).Result;
            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            
            var gitHubEvents = JsonConvert.DeserializeObject <List<GitHubEvent>>(responseString);

            return gitHubEvents;
        }
    }
}
