using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        public Transaction(int UserId)
        { 
            this.TransactionDate = DateTime.Now;
            this.UserId = UserId;
        }

        [Key, Required]
        public int Id_transaction { get; private set; }

        [Required]
        public DateTime TransactionDate { get; private set; }

        [Required]
        public int UserId { get; private set; }
        public User User { get; set; }

        public double GetTotalAmount()
        {
            return this.TransactionItems.Sum(ti => ti.Items.Price);
        }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}