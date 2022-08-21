using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Caltalog.Contacts;
using CommonService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Play.Catelog.Service;
using Play.Catelog.Service.Dtos;
using Play.Catelog.Service.Entities;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        
        private readonly IRepository<Item> itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;
        public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint){
            this.itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetById()
        {
            var items = (await itemsRepository.GetAllAsync())
                        .Select(c=>c.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public  async Task<ItemDto> GetById(Guid id)
        {
            var items = await itemsRepository.GetAsync(id);
            return items.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item  = new Item{Name = createItemDto.Name, Description = createItemDto.Description, Price = createItemDto.Price, CreatedDate = DateTimeOffset.UtcNow};
            await itemsRepository.CreateAsync(item);
            //Here we are publishing the message in the brocker so that other services can subscribe it
            await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction(nameof(GetById), new{id = item.Id});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = (await itemsRepository.GetAsync(id));
            if (existingItem == null) {
                return NotFound();
            }
            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;
            await itemsRepository.UpdateAsync(existingItem);

            //Here we are publishing the message in the brocker so that other services can subscribe it
            await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await itemsRepository.RemoveAsync(id);

            //Here we are publishing the message in the brocker so that other services can subscribe it
            await publishEndpoint.Publish(new CatalogItemDeleted(id));

            return NoContent();
        }
    }
}