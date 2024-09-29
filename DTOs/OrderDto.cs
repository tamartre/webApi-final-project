namespace DTOs
{
    public class OrderDto
    {

        public int UserId { get; set; }

        public int ?OrderSum { get; set; }

        public virtual ICollection<OrderItemDto> OrderItemsDto { get; set; } = new List<OrderItemDto>();
    }
}