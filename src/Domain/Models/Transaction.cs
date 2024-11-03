using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        public Transaction() => TransactionDate = DateTime.Now;

        [Key, Required]
        public int Id_transaction { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public double GetTotalAmount()
        {
            return TransactionItems.Sum(ti => ti.Items.Price);
        }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}