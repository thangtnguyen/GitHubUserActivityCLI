using Microsoft.Extensions.Options;
using Models;
using Models.Interfaces;
using Newtonsoft.Json;
using System.Runtime.InteropServices.Marshalling;
using System.Web;

namespace Services
{
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubInfoOption _gitHubInfo;

        public GithubService(HttpClient httpClient, IOptions<GitHubInfoOption> gitHubInfo)
        {
            _httpClient = httpClient;
            _gitHubInfo = gitHubInfo.Value;
        }

        public List<GitHubEvent> GetRecentGithubEventsByUserName(string userName)
        {
            //_httpClient.BaseAddress = new Uri(string.Format("https:////api.github.com//users//{0}//events", userName));            
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            var response = _httpClient.GetAsync(string.Format(_gitHubInfo.Url + "{0}/events", HttpUtility.UrlEncode(userName))).Result;
            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            
            var gitHubEvents = JsonConvert.DeserializeObject <List<GitHubEvent>>(responseString);

            return gitHubEvents ?? new();
        }
    }
}
