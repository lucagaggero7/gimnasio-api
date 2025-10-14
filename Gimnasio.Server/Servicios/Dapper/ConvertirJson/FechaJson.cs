using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gimnasio.Server.Services.Dapper.ConvertirJson
{
    public class FechaJson : JsonConverter<DateOnly>
    {
        private readonly string _formato = "dd/MM/yyyy";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valor = reader.GetString();
            return DateOnly.ParseExact(valor!, _formato);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_formato));
        }
    }
}
