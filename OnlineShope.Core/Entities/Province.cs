using System.ComponentModel.DataAnnotations;

namespace OnlineShope.Core.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
