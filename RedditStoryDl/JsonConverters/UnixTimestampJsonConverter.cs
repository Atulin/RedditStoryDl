using System.Text.Json;
using System.Text.Json.Serialization;

namespace RedditStoryDl.JsonConverters;

public class UnixTimestampJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) 
        => reader.TryGetDecimal(out var timestamp)
            ? DateTimeOffset.UnixEpoch.AddSeconds((ulong)timestamp)
            : DateTimeOffset.MinValue;

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        => writer.WriteNumberValue(value.ToUnixTimeSeconds());
}