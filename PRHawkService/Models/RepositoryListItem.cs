using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PRHawkService.Models
{
    public class RepositoryListItem
    {

        [Description("Repository Name")]
        [JsonPropertyName("full_name")]
        public string RepoName { get; set; }

        [Description("Repository Link")]
        [JsonPropertyName("html_url")]
        public string RepoLink { get; set; }

        [Description("Pull Request Number")]
        public int? PullRequestNum { get; set; }
    }
}
