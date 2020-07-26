using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logdtore.Domain.Model;
using Logstore.Domain.Interfaces;
using Logstore.Domain.Services;
using Logstore.Domain.Validations;
using Logstore.Infrastructure.Notifiers;

namespace Logstore.Data.Repository
{
    public class FlavorRepository : IFlavorRepository
    {
        private readonly RepositoryBase _context;
        private readonly INotifier _notifier;
        private readonly BaseService _baseService;
        public FlavorRepository(
            INotifier notifier,
            RepositoryBase baseRepository) 
        {
            _context = baseRepository;
            _notifier = notifier;
            _baseService = new BaseService(_notifier);
        }

        private async Task Validation(Flavor flavor)
        {
            if (!_baseService.ModelValidation(new FlavorValidation(), flavor)) return;
            var search = await _context.ExecuteScalar<int>("Select Count(*) from Flavor where Name =@name and Id <> @id", new { name =flavor.Name, id =flavor.Id });
            if (search>0)
            {
                _notifier.SetNotification(new Notification("Esse sabor já esta cadastrado!"));
                return;
            }

        }
        private async Task Relationships(long flavorId)
        {            
            var result = await _context.ExecuteScalar<int>("Select Count(*) from OrderItemFlavor where FlavorId =@flavorId", new { flavorId });
            if(result > 0)
            {
                _notifier.SetNotification(new Notification("Esse sabor não pode ser excluido por esta relacionado com uma ou mais tabelas!"));
                return;
            }            
        }
        public async Task Insert(Flavor flavor)
        {            
            try
            {
                await Validation(flavor);
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Insert(flavor);  
            }
            catch (Exception ex)
            {
                _notifier.SetNotification(new Notification("Não foi possível salvar esse registro, veja o erro:" + ex.Message));
            }            
        }

        public async Task Update(Flavor flavor)
        {           
            try
            {
                await Validation(flavor);
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Update(flavor);                
            }
            catch (Exception ex)
            {
                _notifier.SetNotification(new Notification("Não foi possível salvar esse registro, veja o erro:" + ex.Message));
            }
        }

        public async Task Delete(long id)
        {  
            try
            {
                await Relationships(id);
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Execute("Delete From Flavor Where Id =@id", new { id });
            }
            catch (Exception ex)
            {
                _notifier.SetNotification(new Notification("Não foi possível excluir esse registro, veja o erro:" + ex.Message));
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
        }   
    }
}
