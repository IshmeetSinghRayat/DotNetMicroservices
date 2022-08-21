using System.Globalization;
using Play.Catelog.Service.Dtos;
using Play.Catelog.Service.Entities;

namespace Play.Catelog.Service
{
    public static class Extensions{
        public static ItemDto AsDto(this Item item){
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}