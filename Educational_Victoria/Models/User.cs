using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Educational_Victoria.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 
        public string PhoneNumber { get; set; }
        public EnumRoles Role {  get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<UserSubjectAccess> UserAccesses { get; set; }
    }
}
