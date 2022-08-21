using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Caltalog.Contacts;
using CommonService;
using InventoryService.Entities;
using MassTransit;

namespace InventoryService.Consumers
{
    public class CatelogItemDeleteConsumer : IConsumer<CatalogItemDeleted>
    {
        private readonly IRepository<CatalogItem> repository;
        public CatelogItemDeleteConsumer(IRepository<CatalogItem> repository){
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.ItemId);
            if (item == null){
                return;
            }
            await repository.RemoveAsync(message.ItemId);
        }
    }
}
