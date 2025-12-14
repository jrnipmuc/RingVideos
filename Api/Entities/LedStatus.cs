using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KoenZomers.Ring.Api.Entities
{
    public class LedStatus
    {
        [JsonPropertyName("seconds_remaining")]
        public int? SecondsRemaining { get; set; }

        public string Status { get; set; }

        public bool IsOn => Status?.Equals("on", StringComparison.OrdinalIgnoreCase) ?? false;
        public bool IsOff => Status?.Equals("off", StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public class LedStatusConverter : JsonConverter<LedStatus>
    {
        public override LedStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                // Handle string case: "on" or "off"
                var status = reader.GetString();
                return new LedStatus { Status = status };
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Handle object case: {"seconds_remaining":0}
                using var doc = JsonDocument.ParseValue(ref reader);
                var ledStatus = new LedStatus();

                if (doc.RootElement.TryGetProperty("seconds_remaining", out var secondsRemainingElement))
                {
                    ledStatus.SecondsRemaining = secondsRemainingElement.GetInt32();
                }

                return ledStatus;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, LedStatus value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            if (!string.IsNullOrEmpty(value.Status))
            {
                // Write as string if Status is set
                writer.WriteStringValue(value.Status);
            }
            else
            {
                // Write as object if SecondsRemaining is set
                writer.WriteStartObject();
                if (value.SecondsRemaining.HasValue)
                {
                    writer.WriteNumber("seconds_remaining", value.SecondsRemaining.Value);
                }
                writer.WriteEndObject();
            }
        }
    }

}
