namespace OnlineShope.Applicaition.Models
{
    public class ProvinceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<CityDto> City { get; set; }
    }
}
