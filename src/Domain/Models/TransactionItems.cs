namespace Domain.Models
{
    public class TransactionItems
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int ItemsId { get; set; }
        public Items Items { get; set; }
    }
}