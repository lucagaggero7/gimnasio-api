using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gimnasio.Server.Services.Dapper.ConvertirJson
{
    public class HoraJson : JsonConverter<TimeOnly>
    {

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valor = reader.GetString();
            if (string.IsNullOrWhiteSpace(valor))
                return default;

            if (TimeOnly.TryParseExact(valor, "HH:mm:ss", out var time))
                return time;

            if (TimeOnly.TryParseExact(valor, "HH:mm", out time))
                return time;

            throw new FormatException($"Formato de hora inválido: {valor}");
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            if (value.Second == 0)
                writer.WriteStringValue(value.ToString("HH:mm"));
            else
                writer.WriteStringValue(value.ToString("HH:mm:ss"));
        }
    }
}
