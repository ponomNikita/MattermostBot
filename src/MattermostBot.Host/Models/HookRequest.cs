using Newtonsoft.Json;

namespace MattermostBot.Host.Models
{
    public class HookRequest
    {
        [JsonProperty(PropertyName = "channel_id")]
        public string ChannelId { get; set; }

        [JsonProperty(PropertyName = "channel_name")]
        public string ChannelName { get; set; }

        [JsonProperty(PropertyName = "team_domain")]
        public string TeamDomain { get; set; }

        [JsonProperty(PropertyName = "post_id")]
        public string PostId { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "trigger_word")]
        public long TriggerWord { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public long UserId { get; set; }

        [JsonProperty(PropertyName = "user_name")]
        public long UserName { get; set; }

        [JsonProperty(PropertyName = "file_ids")]
        public long FileIds { get; set; }
    }
}