using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Items
    {
        [Key, Required]
        public int Id_items { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}
