using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PRHawkService.Models
{
    public class PullRequestResponse
    {

        //[Description("Pulls Request Number")]
        //[JsonPropertyName("total_count")]
        public int? total_count { get; set; }

        public static implicit operator Task<object>(PullRequestResponse v)
        {
            throw new NotImplementedException();
        }
    }
}
