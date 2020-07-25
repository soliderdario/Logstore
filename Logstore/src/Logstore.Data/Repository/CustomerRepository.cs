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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RepositoryBase _context;
        private readonly INotifier _notifier;
        private readonly BaseService _baseService;
        public CustomerRepository(
            INotifier notifier,
            RepositoryBase baseRepository)
        {
            _context = baseRepository;
            _notifier = notifier;
            _baseService = new BaseService(_notifier);
        }

        private async Task Validation(Customer customer)
        {
            if (!_baseService.ExecuteValidation(new CustomerValidation(), customer)) return;
            var search = await _context.ExecuteScalar<int>("Select Count(*) from Customer where Email =@email and Id <> @id", new { email = customer.Email, id = customer.Id });
            if (search > 0)
            {
                _notifier.SetNotification(new Notification("Esse cliente já esta cadastrado!"));
                return;
            }
        }
        private async Task Relationships(long customerId)
        {
            var result = await _context.ExecuteScalar<int>("Select Count(*) from Order where CustomerId =@customerId", new { customerId });
            if (result > 0)
            {
                _notifier.SetNotification(new Notification("Esse cliente não pode ser excluido por esta relacionado com uma ou mais tabelas!"));
                return;
            }
        }
        public async Task Insert(Customer customer)
        {
            try
            {
                await Validation(customer);
                if (_notifier.HasNotification())
                {
                    return;
                }
                await _context.Insert(customer);
            }
            catch (Exception ex)
            {
                _notifier.SetNotification(new Notification("Não foi possível salvar esse registro, veja o erro:" + ex.Message));
            }
        }

        public async Task Update(Customer flavor)
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
                await _context.Execute("Delete From Customer Where Id =@id", new { id });
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