using Dapper;
using System.Data;

namespace CRUD_PracticaProf.Dapper.ManejadorTipos
{
    public class Fecha : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToString("yyyy-MM-dd");
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.Parse(value.ToString());
        }
    }
}
