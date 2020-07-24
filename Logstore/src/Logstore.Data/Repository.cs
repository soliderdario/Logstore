using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Logstore.Data
{
    public class DapperRepository
    {
        private readonly IDbConnection _conn;

        public DapperRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public List<T> Query<T>(string query, object parameters = null)
        {
            return _conn.Query<T>(query, parameters).ToList();
        }

        public int Execute(string query, object parameters = null)
        {
            return _conn.Execute(query, parameters);
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            return _conn.QuerySingle<T>(query, parameters);
        }
        public T QueryFirst<T>(string query, object parameters = null)
        {
            return _conn.QueryFirst<T>(query, parameters);
        }
    }

    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        protected BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected string GetConnection()
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
            return connection;
        }

        public async Task<List<T>> Query<T>(string query, object parameters = null)
        {
            using var con = new SqlConnection(this.GetConnection());
            try
            {
                con.Open();
                return (List<T>)await con.QueryAsync(query, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
