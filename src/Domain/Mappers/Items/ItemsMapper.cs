using Domain.Query.Items;

namespace Domain.Mappers.Items
{
    public sealed record class ItemsMapper
    {
        public GetItemsQuery ToGetItemsMapper(Domain.Models.Items items)
        {
            if(items == null)
            {
                return null;
            }

            var newItems = new GetItemsQuery
            {
                Name = items.Name,
                Description = items.Description,
                Price = items.Price,
                Id_items = items.Id_items,
            };

            return newItems;
        }
    }
}