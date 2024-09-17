namespace Models.Interfaces
{
    public interface IGithubService
    {
        List<GitHubEvent> GetRecentGithubEventsByUserName(string userName);
    }
}
