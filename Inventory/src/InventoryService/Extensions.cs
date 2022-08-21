using System;
using InventoryService.Dtos;
using InventoryService.Entities;

namespace InventoryService
{
    public static class Extensions
    {
        public static InventoryItemsDto AsDto(this InventoryItem item)
        {
            return new InventoryItemsDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
        }
    }
}
