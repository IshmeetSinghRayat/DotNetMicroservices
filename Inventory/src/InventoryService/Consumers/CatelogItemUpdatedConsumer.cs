using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Caltalog.Contacts;
using CommonService;
using InventoryService.Entities;
using MassTransit;

namespace InventoryService.Consumers
{
    public class CatelogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem> repository;
        public CatelogItemUpdatedConsumer(IRepository<CatalogItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.ItemId);
            if (item == null){
                item = new CatalogItem{
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
                };
                await repository.CreateAsync(item);  
            }
            else 
            {
                item.Name = message.Name;
                item.Description = message.Description;
                await repository.UpdateAsync(item);  
            }
        }
    }
}
