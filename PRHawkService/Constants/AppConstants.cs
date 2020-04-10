using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PRHawkService.Constants
{
    public class AppConstants
    {
        public static class StatusCode
        {
            public const int
             OK = (int)HttpStatusCode.OK,
             NotFound = (int)HttpStatusCode.NotFound,
             BadRequest = (int)HttpStatusCode.BadRequest,
             UnknownError = 701,
             UnprocessableEntityStatus = 422, //Request is but failed validation
             Unauthorized = (int)HttpStatusCode.Unauthorized,
             UnsupportedMediaType = (int)HttpStatusCode.UnsupportedMediaType;
        }
        public static class StatusMessage
        {
            public const string
                OK = "Success",
                NotFound = "Not found",
                UnknownError = "Something went wrong",
                InvalidParam = "Invalid parameter",
                NullRequest = "Null request parameter",
                UnsupportedMediaType = "Unsupported MediaType",
                NoRecordsFound = "No records found";
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum StateOfPull
        {
            open,
            closed,
            all
        }
    }
}
