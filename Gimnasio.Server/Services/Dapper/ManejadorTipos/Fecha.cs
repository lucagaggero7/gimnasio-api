using Dapper;
using System.Data;

namespace Gimnasio.Server.Services.Dapper.ManejadorTipos
{
    public class Fecha : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
          => parameter.Value = value.ToDateTime(TimeOnly.MinValue);

        public override DateOnly Parse(object value)
            => DateOnly.FromDateTime((DateTime)value);
    }
}
