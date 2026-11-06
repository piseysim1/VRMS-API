using Newtonsoft.Json;

namespace vrms_api.Models
{
    public class ShareModel
    {
        public class ApiResponse<T>
        {
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }
            [JsonProperty("type")]
            public ResponseType ResponseType { get; set; } = ResponseType.Success;
            [JsonProperty("result")]
            public T Result { get; set; }
        }

        public enum ResponseType { Error = 1, Success = 2 }
        public class RemoveModel
        {
            public long Id { get; set; }
            public string Note { get; set; }
        }
        public class MessageResult
        {
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }
            [JsonProperty("type")]
            public ResponseType ResponseType { get; set; } = ResponseType.Success;
        }
        public class ViewResult<TResult> : MessageResult
        {
            [JsonProperty("result")]
            public TResult Result { get; set; }
        }
        public class SearchResult<TResult, TSearch>
        {
            [JsonProperty("results")]
            public List<TResult> Results { get; set; }
            [JsonProperty("param")]
            public TSearch Param { get; set; }
        }
        public class Sort
        {
            public string Field { get; set; }
            public string Dir { get; set; }
        }
    }
}
