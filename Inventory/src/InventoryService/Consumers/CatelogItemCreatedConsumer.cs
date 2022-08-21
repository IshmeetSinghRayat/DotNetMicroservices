using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Caltalog.Contacts;
using CommonService;
using InventoryService.Entities;
using MassTransit;

namespace InventoryService.Consumers
{
    public class CatelogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {
        private readonly IRepository<CatalogItem> repository;
        public CatelogItemCreatedConsumer(IRepository<CatalogItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.ItemId);
            if (item != null){
                return;
            }
            item = new CatalogItem{
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };
            await repository.CreateAsync(item);

        }
    }
}
