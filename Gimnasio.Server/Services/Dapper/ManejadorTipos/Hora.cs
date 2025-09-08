using Dapper;
using System.Data;

namespace Gimnasio.Server.Services.Dapper.ManejadorTipos
{
    public class Hora : SqlMapper.TypeHandler<TimeOnly>
    {
        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
           => parameter.Value = value.ToTimeSpan();

        public override TimeOnly Parse(object value)
            => TimeOnly.FromTimeSpan((TimeSpan)value);
    }
}
