using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MattermostBot.Host.Models
{
    public class HookResponse
    {
        /// <summary>
        /// Markdown-formatted message to display in the post.
        /// To trigger notifications, use @<username>, @channel and @here like you would in normal Mattermost messaging.
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Set to “comment” to reply to the message that triggered it.
        /// Set to blank or “post” to create a regular message.
        /// Defaults to “post”.
        /// </summary>
        [JsonProperty(PropertyName = "response_type")]
        public string ResponseType { get; set; }

        /// <summary>
        /// Overrides the username the message posts as.
        /// Defaults to the username set during webhook creation or the webhook creator's username if the former was not set.
        /// Must be enabled in the configuration.
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Overrides the profile picture the message posts with.
        /// Defaults to the URL set during webhook creation or the webhook creator's profile picture if the former was not set.
        /// Must be enabled in the configuration.
        /// </summary>
        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; set; }

        // ToDo Attachmets https://developers.mattermost.com/integrate/outgoing-webhooks/

        /// <summary>
        /// Sets the post type, mainly for use by plugins.
        /// If not blank, must begin with “custom_". Passing attachments will ignore this field and set the type to slack\_attachment.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Sets the post props, a JSON property bag for storing extra or meta data on the post.
        /// Mainly used by other integrations accessing posts through the REST API.
        /// The following keys are reserved: “from_webhook”, “override_username”, “override_icon_url”, “webhook_display_name” and “attachments”.
        /// </summary>
        [JsonProperty(PropertyName = "props")]
        public string Props { get; set; }
    }
}