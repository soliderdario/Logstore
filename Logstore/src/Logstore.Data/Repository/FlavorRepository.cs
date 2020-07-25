using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logdtore.Domain.Model;
using Logstore.Domain.Interfaces;

namespace Logstore.Data.Repository
{
    public class FlavorRepository : IFlavorRepository
    {
        private readonly RepositoryBase _context;
        public FlavorRepository(RepositoryBase baseRepository) 
        {
            _context = baseRepository;
        }
        public async Task<long> Insert(Flavor flavor)
        {            
            try
            {                
                return await _context.Insert(flavor);  
            }
            catch (Exception ex)
            { 
                throw ex;
            }
            finally
            {
                
            }
        }

        public async Task Update(Flavor flavor)
        {           
            try
            {
                _context.Begin();
                await _context.Update(flavor);
                Convert.ToInt32("ooo");
                _context.Commit();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw ex;
            }
            finally
            {
                
            }
        }

        public async Task Delete(long id)
        {  
            try
            {                
                await _context.Execute("Delete From Flavor Where Id =@id", new { id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
        
        public async Task<IEnumerable<T>> Query<T>(string query, object parameters = null)
        {
            try
            {
                return await _context.Query<T>(query, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }        
        
    }
}
