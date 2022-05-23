using System.ComponentModel.DataAnnotations;

namespace OnlineShope.Core.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(32)]
        public string FirstName { get; set; }
        [Required, MaxLength(64)]
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
