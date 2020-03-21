using System.Text.Json.Serialization;

namespace MattermostBot.Host.Models
{
    public class HookRequest
    {
        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("channel_name")]
        public string ChannelName { get; set; }

        [JsonPropertyName("team_domain")]
        public string TeamDomain { get; set; }

        [JsonPropertyName("post_id")]
        public string PostId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("trigger_word")]
        public string TriggerWord { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("file_ids")]
        public string FileIds { get; set; }
    }
}