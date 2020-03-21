using System;
using Newtonsoft.Json;

namespace MattermostBot.ArtifactoryClient
{
    public class Artifact
    {
        [JsonProperty("repo")]
        public string Repo { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }

        [JsonProperty("modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }
    }
    
}
