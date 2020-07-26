using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logdtore.Domain.Model;
using Logdtore.Domain.View;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IFlavorRepository _flavorRepository;
        private readonly List<Flavor> _flavors = new List<Flavor>();
        public OrderRepository(
            ICustomerRepository customerRepository,
            IFlavorRepository flavorRepository,
            INotifier notifier,
            RepositoryBase baseRepository)
        {
            _context = baseRepository;
            _notifier = notifier;
            _baseService = new BaseService(_notifier);
            _customerRepository = customerRepository;
            _flavorRepository = flavorRepository;
        }

        private async Task Validation(OrderView order)
        {
            if (!_baseService.ViewValidation(new OrderValidation(), order)) return;            

            var selectFlavors = order.Items.Select(src => src.Flavors);
            foreach(var idFlavors in selectFlavors)
            {
                foreach(var flavorId in idFlavors)
                {
                    var flavors = await _flavorRepository.Query<Flavor>("Select * from Flavor where Id =@flavorId", new { flavorId });
                    if(!flavors.Any())
                    {
                        _notifier.SetNotification(new Notification("Sabor não encontrado"));
                        return;
                    }
                    _flavors.Add(flavors.FirstOrDefault());
                }
            }
        } 

        public async Task Save(OrderView orderView, Customer customer)
        {
            try
            {  

               await Validation(orderView);
                if (_notifier.HasNotification())
                {
                    return;
                }
                _context.Begin();

                //Prepare order
                var order = new Order
                {
                    DateCreate = orderView.DateCreate
                };

                //Create customer or get your Id
                var customers = await _customerRepository.Query<long>("Select Id from Customer where email =@email", new { email = orderView.Email });
                if (!customers.Any())
                {
                    //Create customer
                    await _customerRepository.Insert(customer);
                    order.CustomerId = customer.Id;
                } else
                {
                    //Ger your Id
                    order.CustomerId = customers.ToList().FirstOrDefault();
                }
                
                // save order
                await _context.Insert(order);

                // save items
                foreach (var item in orderView.Items)
                {
                    var orderitem = new OrderItem
                    {
                        OrderId = order.Id,
                    };
                    await _context.Insert(orderitem);

                    // save flavors each item
                    foreach(var floavorId in item.Flavors)
                    {
                        var flavor = _flavors.Where(src => src.Id == floavorId).FirstOrDefault();
                        var orderitemFlavor = new OrderItemFlavor
                        {
                            OrderItemId = orderitem.Id,
                            FlavorId = flavor.Id,
                            Value = flavor.Price
                        };
                        await _context.Insert(orderitemFlavor);
                    }
                }
                _context.Commit();
                
            }
            catch (Exception ex)
            {
                _context.Rollback();
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