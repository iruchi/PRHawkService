using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PRHawkService.Models
{
    public class UserRepositoryRequest
    {
        [Description("Page Number")]
        [JsonPropertyName("pagenum")]
        public int PageNum { get; set; }

        [Description("Number of Records")]
        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}
