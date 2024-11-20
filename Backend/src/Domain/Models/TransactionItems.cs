namespace Domain.Models
{
    public class TransactionItems
    {
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Guid ItemsId { get; set; }
        public Items Items { get; set; }
    }
}