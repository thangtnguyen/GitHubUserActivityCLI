using Models.Interfaces;

namespace github_activity
{
    public class Application : IApplication
    {
        private readonly IGithubService _githubService;
        private Dictionary<string, string> _messageMapper = new Dictionary<string, string>();

        public Application(IGithubService gitHubService)
        {
            _githubService = gitHubService;
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            _messageMapper.Add("CommitCommentEvent", "- Created {0} commit comment(s) in {1}.");
            _messageMapper.Add("CreateEvent", "- Created {0} Git branch(s) or tag(s) in {1}.");
            _messageMapper.Add("DeleteEvent", "- Deleted {0} Git branch(s) or tag(s) in {1}.");
            _messageMapper.Add("ForkEvent", "- Forked {0} repository(ies).");
            _messageMapper.Add("GollumEvent", "- Created or updated {0} wiki page(s) in {1}.");
            _messageMapper.Add("IssueCommentEvent", "- Created {0} issues or pull request comment(s) in {1}.");
            _messageMapper.Add("IssuesEvent", "- Created {0} activity(ies) related to issue in {1}.");
            _messageMapper.Add("MemberEvent", "- Created {0} activity(ies) related to repository collaborators {1}.");
            _messageMapper.Add("PublicEvent", "- Maked {0} repository(ies) to public.");
            _messageMapper.Add("PullRequestEvent", "- Created {0} activity(ies) ralated to pull requests in {1}.");
            _messageMapper.Add("PullRequestReviewEvent", "- Created {0} activity(ies) related to pull request reviews in {1}");
            _messageMapper.Add("PullRequestReviewCommentEvent", "- Created {0} activity(ies) related to pull request review comments in the pull request's unified diff.");
            _messageMapper.Add("PullRequestReviewThreadEvent", "- Created {0} activity(ies) related to a comment thread on a pull request being marked as resolved or unresolved.");
            _messageMapper.Add("PushEvent", "- Pushed {0} commits to {1}.");
            _messageMapper.Add("ReleaseEvent", "- Created {0} activity(ies) related to a release in {1}.");
            _messageMapper.Add("SponsorshipEvent", "- Created {0} activity(ies) related to a sponsorship listing in {1}");
            _messageMapper.Add("WatchEvent", "- Starred {0} times to {1}");
        }

        public void HandleBusiness(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please use github-activity <username>");
            }
            else
            {
                try
                {
                    var result = _githubService.GetRecentGithubEventsByUserName(args[0]);

                    if (result != null)
                    {
                        var groups = result.GroupBy(p => new { p.Repo.Name, p.Type }).ToList();
                        foreach (var grp in groups)
                        {
                            if (grp.Key.Type == "ForkEvent")
                            {
                                Console.WriteLine(string.Format(_messageMapper[grp.Key.Type], grp.Key.Name));
                            }
                            else
                            {
                                Console.WriteLine(string.Format(_messageMapper[grp.Key.Type], grp.Count(), grp.Key.Name));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error happens. Please try again.");                    
                }
            }
        }
    }
}
