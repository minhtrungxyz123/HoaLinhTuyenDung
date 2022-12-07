using Dapper;
using System.Data;
using System.Data.Common;

namespace TuyenDung.Data.Dapper
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();

        Task<T> GetAyncFirst<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetList<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<IEnumerable<T>> GetAllAync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        IEnumerable<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}