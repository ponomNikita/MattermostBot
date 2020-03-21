using System.Collections.Generic;
using Newtonsoft.Json;

namespace MattermostBot.ArtifactoryClient
{
    public class ArtifactsList
    {
        [JsonProperty("results")]
        public List<Artifact> Results { get; set; } = new List<Artifact>();
    }
    
}
