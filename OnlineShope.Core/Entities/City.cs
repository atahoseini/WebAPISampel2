using System.ComponentModel.DataAnnotations;

namespace OnlineShope.Core.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

    }
}
