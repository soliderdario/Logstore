using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Dapper.Contrib.Extensions;
using Logdtore.Domain;
using Logdtore.Domain.Model;
using Logdtore.Domain.Interfaces;

namespace Logstore.Data.Repository
{
    public class FlavorRepository : BaseRepository, IFlavorRepository
    {
        public FlavorRepository(IConfiguration configuration) :base(configuration)
        {
           
        }
        public async Task<long> Insert(Flavor flavor)
        {
            using var con = new SqlConnection(this.GetConnection());
            try
            {
                con.Open();
                return await con.InsertAsync(flavor);
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

        public async Task Update(Flavor flavor)
        {
            using var con = new SqlConnection(this.GetConnection());
            try
            {
                con.Open();
                await con.UpdateAsync(flavor);
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

        public async Task Delete(long id)
        {            
            using var con = new SqlConnection(this.GetConnection());
            try
            {
                con.Open();
                await con.ExecuteAsync("Delete From Flavor Where Id =@id", new { id });
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
        
        public new async Task<List<Flavor>> Query<Flavor>(string query, object parameters = null)
        {
            return await base.Query<Flavor>(query, parameters);
        }
    }
}
