using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRUD_PracticaProf.Dapper.ConvertirJson
{
    public class HoraJson : JsonConverter<TimeOnly>
    {
        private readonly string _formato = "HH:mm:ss";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valor = reader.GetString();
            return TimeOnly.ParseExact(valor!, _formato);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_formato));
        }
    }
}
