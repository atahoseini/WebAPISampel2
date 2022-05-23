using System.ComponentModel.DataAnnotations;

namespace OnlineShope.Core.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100), Required]
        public int SupplierName { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
