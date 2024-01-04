using System.Text.Json.Serialization;

namespace Vox.Utils
{
    public class Error
    {
        /// <summary>
        ///     One of a server-defined set of error codes.
        /// </summary>
        [JsonPropertyName("status_code")]
        public int statusCode { get; set; } = 500;

        /// <summary>
        ///     A human-readable representation of the error.
        /// </summary>
        [JsonPropertyName("message")]
        public string message { get; set; } = null;
    }
}
