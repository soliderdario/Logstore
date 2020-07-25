using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using Logdtore.Domain.Model;

namespace Logstore.Data
{
    public class RepositoryBase 
    {
        private readonly int _timeOut = 200;
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction = null;

        public RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
            if (_dbConnection == null)
            {
                _dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }

            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
        }        

        public async Task<IEnumerable<T>> Query<T>(string query, object parameters = null)
        {                
            var result = await _dbConnection.QueryAsync<T>(query, parameters);
            return (IEnumerable<T>)result;
        }

        public Task<int> Execute(string query, object parameters = null)
        {
            return _dbConnection.ExecuteAsync(query, parameters, _dbTransaction);
        }

        public async Task<T> ExecuteProcedureScalar<T>(string name, object parameters)
        {
            var result = _dbConnection.ExecuteScalarAsync<T>(name, parameters, _dbTransaction, commandType: CommandType.StoredProcedure, commandTimeout: _timeOut);
            return await result;
        }

        public async Task<T> ExecuteScalarAsync<T>(string name, object parameters)
        {
            return await _dbConnection.ExecuteScalarAsync<T>(name, parameters, _dbTransaction);           
        }

        public async Task ExecuteProcedureAsync(string name, object parameters)
        {
            await _dbConnection.ExecuteAsync(name, parameters, commandType: CommandType.StoredProcedure, commandTimeout: _timeOut);
        }

        public async Task<int> Insert<T>(T entity) where T : ModelBase
        {
             return  await _dbConnection.InsertAsync(entity, _dbTransaction);
        }

        public async Task<bool> Update<T>(T entity) where T: ModelBase
        {
            return await _dbConnection.UpdateAsync(entity, _dbTransaction);
        }

        public void Begin()
        {
            _dbTransaction = _dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }

    }
}
