using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PRHawkService.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        [JsonPropertyName("code")]
        [Description("Status Code")]
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Contains message based on code
        /// </summary>
        [JsonPropertyName("message")]
        [Description("Contains message based on code")]
        public string Message { get; set; }

        /// <summary>
        /// Contains data section
        /// </summary>
        [JsonPropertyName("data")]
        [Description("Contains data section")]
        public T Data { get; set; }
    
    }
}
