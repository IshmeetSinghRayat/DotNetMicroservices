using System;
using CommonService;

namespace Play.Catelog.Service.Entities
{
    public class Item: IEntiry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}