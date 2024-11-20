using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Items
    {
        public Items() { }
        public Items(Guid itemsId, string name, string description, double price, DateTime CreatedAt) 
        {
            this.Id_items = itemsId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.CreatedAt = CreatedAt;
        }

        [Key, Required]
        public Guid Id_items { get; private set; }
        [Required]

        public string Name { get; private set; }
        [Required]

        public string Description { get; private set; }
        [Required]
        public double Price { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public ICollection<TransactionItems> TransactionItems { get; set; }
    }
}