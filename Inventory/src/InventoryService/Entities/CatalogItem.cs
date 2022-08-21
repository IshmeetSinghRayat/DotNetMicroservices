using System;
using CommonService;

namespace InventoryService.Entities
{
    public class CatalogItem : IEntiry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
