using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly BaseService _baseService;
        private readonly List<Flavor> _flavors = new List<Flavor>();
        private Customer _customer = null;
        private readonly INotifier _notifier;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFlavorRepository _flavorRepository;
        private readonly IMapper _mapper;

        public OrderRepository(
            ICustomerRepository customerRepository,
            IFlavorRepository flavorRepository,
            INotifier notifier,
            IMapper mapper,
            RepositoryBase baseRepository)
        {
            _context = baseRepository;
            _notifier = notifier;
            _mapper = mapper;
            _baseService = new BaseService(_notifier);
            _customerRepository = customerRepository;
            _flavorRepository = flavorRepository;
        }

        private async Task PrepareFlavors(IEnumerable<List<long>> selectFlavors)
        {
            foreach (var idFlavors in selectFlavors)
            {
                foreach (var flavorId in idFlavors)
                {
                    var flavors = await _flavorRepository.Query<Flavor>("Select * from Flavor nolock where Id =@flavorId", new { flavorId });
                    if (!flavors.Any())
                    {
                        _notifier.SetNotification(new Notification("Sabor não encontrado"));
                        return;
                    }
                    _flavors.Add(flavors.FirstOrDefault());
                }
            }
        }

        private async Task Validation(OrderYesCustomerView order)
        {
            if (!_baseService.ViewValidation(new OrderYesCustomerValidation(), order)) return;

            var customers = await _customerRepository.Query<Customer>("Select * from Customer nolock where email =@email", new { email = order.Email });
            if (!customers.Any())
            {
                _notifier.SetNotification(new Notification("Cliente não encontrado"));
                return;
            }
            _customer = customers.FirstOrDefault();

            var selectFlavors = order.Items.Select(src => src.Flavors);
            await PrepareFlavors(selectFlavors);
        }

        private async Task Validation(OrderNoCustomerView order)
        {
            if (!_baseService.ViewValidation(new OrderValidation(), order)) return;

            var customers = await _customerRepository.Query<Customer>("Select * from Customer nolock where email =@email", new { email = order.Email });
            if (customers.Any())
            {
                _customer = customers.FirstOrDefault();                
            }

            var selectFlavors = order.Items.Select(src => src.Flavors);
            await PrepareFlavors(selectFlavors);
        }

        private async Task Save(DateTime dateTime, IEnumerable<OrderItemView> orderItem)
        {
            //Prepare order
            var order = new Order
            {
                DateCreate = dateTime,
                CustomerId = _customer.Id
            };

            // save order
            await _context.Insert(order);

            // save order Address Delivery
            var orderAddressDelivery = _mapper.Map<OrderAddressDelivery>(_customer);
            orderAddressDelivery.OrderId = order.Id;
            await _context.Insert(orderAddressDelivery);


            // save items
            foreach (var item in orderItem)
            {
                var orderitem = new OrderItem
                {
                    OrderId = order.Id,
                };
                await _context.Insert(orderitem);

                // save flavors each item
                foreach (var floavorId in item.Flavors)
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
        }

        public async Task Save(OrderYesCustomerView orderView)
        {
            try
            {                
                await Validation(orderView);
                if (_notifier.HasNotification())
                {
                    return;
                }
                _context.Begin();
                await Save(orderView.DateCreate, orderView.Items);                
                _context.Commit();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                _notifier.SetNotification(new Notification("Não foi possível salvar esse registro, veja o erro:" + ex.Message));
            }
        }

        public async Task Save(OrderNoCustomerView orderView)
        {
            try
            {  

               await Validation(orderView);
                if (_notifier.HasNotification())
                {
                    return;
                }
                _context.Begin();

                //Create customer             
                if (_customer == null)
                {
                    var customer = _mapper.Map<Customer>(orderView);
                    await _customerRepository.Insert(customer);
                    _customer = customer;
                }
                else
                {
                    // The customer exists, but the delivery can be in another location
                    var name = _customer.Name;
                    _customer = _mapper.Map<Customer>(orderView);
                    _customer.Name = name;
                }
                await Save(orderView.DateCreate, orderView.Items);                
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