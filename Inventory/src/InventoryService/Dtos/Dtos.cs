using System;

namespace InventoryService.Dtos
{
  public record GrantItemsDto(Guid UserId, Guid CatelogItemId, int Quantity);
  public record InventoryItemsDto(Guid CatalogItemId, int Quantity, DateTimeOffset AcquiredDate);
  public record InventoryItemsDetailsDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);
  
}
