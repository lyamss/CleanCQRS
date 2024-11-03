using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Transaction
    {
        [Key, Required]
        public int Id_transaction { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public double GetTotalAmount()
        {
            return 1;
        }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}