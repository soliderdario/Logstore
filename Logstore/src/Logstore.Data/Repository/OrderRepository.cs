using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logstore.Domain.Interfaces;
using Logstore.Domain.Model;
using Logstore.Domain.Services;
using Logstore.Domain.Validations;
using Logstore.Infrastructure.Notifiers;

namespace Logstore.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RepositoryBase _context;
        private readonly INotifier _notifier;
        private readonly BaseService _baseService;
        public OrderRepository(
            INotifier notifier,
            RepositoryBase baseRepository)
        {
            _context = baseRepository;
            _notifier = notifier;
            _baseService = new BaseService(_notifier);
        }

        private async Task Validation(Order order)
        {
            if (!_baseService.ExecuteValidation(new OrderValidation(), order)) return;            
        }
        
        public async Task Insert(Order order)
        {
            try
            {
                await Validation(order);
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Insert(order);
            }
            catch (Exception ex)
            {
                _notifier.SetNotification(new Notification("Não foi possível salvar esse registro, veja o erro:" + ex.Message));
            }
        }

        public async Task Update(Order flavor)
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
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Execute("Delete From Order Where Id =@id", new { id });
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