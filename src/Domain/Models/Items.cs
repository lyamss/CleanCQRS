using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Items
    {
        public Items(string name, string description, double price) 
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        [Key, Required]
        public int Id_items { get; private set; }
        [Required]

        public string Name { get; private set; }
        [Required]

        public string Description { get; private set; }
        [Required]
        public double Price { get; private set; }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}
