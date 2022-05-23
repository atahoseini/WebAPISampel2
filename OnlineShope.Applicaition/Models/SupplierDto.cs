namespace OnlineShope.Applicaition.Models
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public int SupplierName { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
