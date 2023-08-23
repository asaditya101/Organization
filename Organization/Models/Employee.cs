using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organization.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = "";
        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; } = "";
        [Required]
        public int Age { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string Address { get; set; } = "";
    }
}