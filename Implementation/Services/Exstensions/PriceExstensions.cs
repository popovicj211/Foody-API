using Domain.Entities;

namespace Implementation.Services.Exstensions
{
    public static class PriceExstensions
    {
        public static object GetPrice(this BaseEntity entity, int id, string property)
        {
            var price = entity?.GetType()?.GetProperty(property)?.GetValue(entity);
            return price != null ? price : default;
        }
    }
}
