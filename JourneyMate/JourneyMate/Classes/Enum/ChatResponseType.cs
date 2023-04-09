using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JourneyMate.Classes.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChatResponseType
    {
        /// <summary>
        /// Messege response,
        /// Including html and all
        /// </summary>
        [EnumMember(Value = "Text")]
        Text,

        /// <summary>
        /// Send buttons
        /// </summary>
        [EnumMember(Value = "Buttons")]
        Buttons
    }
}
