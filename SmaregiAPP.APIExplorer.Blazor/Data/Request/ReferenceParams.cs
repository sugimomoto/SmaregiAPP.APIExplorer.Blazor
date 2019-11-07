using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmaregiAPP.APIExplorer.Blazor.Data.Request
{
    public class ReferenceParams
    {
        [JsonProperty("fileds")]
        public string[] Fileds { get; set; }

        [JsonProperty("conditions")]
        public Dictionary<string, string>[] Conditions { get; set; }

        [JsonProperty("order")]
        public string[] Order { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("table_name")]
        public string TableName { get; set; }
    }
}
