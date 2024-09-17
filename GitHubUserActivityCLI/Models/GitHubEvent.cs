namespace Models
{
    public class GitHubEvent
    {
        public string Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public required GitHubRepo Repo { get; set; }
    }
}
