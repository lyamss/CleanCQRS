using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        public Transaction () { }
        public Transaction(Guid transactionsId, Guid UserId)
        { 
            this.Id_transaction = transactionsId;
            this.TransactionDate = DateTime.Now;
            this.UserId = UserId;
        }

        [Key, Required]
        public Guid Id_transaction { get; private set; }

        [Required]
        public DateTime TransactionDate { get; private set; }

        [Required]
        public Guid UserId { get; private set; }
        public User User { get; set; }

        public double GetTotalAmount()
        {
            return this.TransactionItems.Sum(ti => ti.Items.Price);
        }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}