using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PRHawkService.Models
{
    public class RepositoryListResponseItem
    {
        //[Description("Repo Name")]
        //[JsonPropertyName("full_name")]
        public string full_name { get; set; }

        //[Description("Repo Link")]
        //[JsonPropertyName("html_url")]
        public string html_url { get; set; }
    }
}
