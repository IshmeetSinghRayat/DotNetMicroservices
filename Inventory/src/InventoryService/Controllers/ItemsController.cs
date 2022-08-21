using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonService;
using InventoryService.Dtos;
using InventoryService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> itemsRepository;
        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }
        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<InventoryItemsDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty){
                return BadRequest();
            }
            
            var items = (await itemsRepository.GetAllAsync(item => item.UserId == userId))
                        .Select(item => item.AsDto());

            return Ok(items);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto){
            var InventoryItem = await itemsRepository.GetAsync(
                item => item.UserId == grantItemsDto.UserId && item.CatalogItemId == grantItemsDto.CatelogItemId
            );
            if (InventoryItem == null){
                InventoryItem = new InventoryItem{
                    CatalogItemId = grantItemsDto.CatelogItemId,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };
                await itemsRepository.CreateAsync(InventoryItem);
            }
            else{
                InventoryItem.Quantity += grantItemsDto.Quantity;
                await itemsRepository.UpdateAsync(InventoryItem);
            }
            return Ok();
        }
    }
}
