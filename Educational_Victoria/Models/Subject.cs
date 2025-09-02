using System.ComponentModel.DataAnnotations;

namespace Educational_Victoria.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsFree { get; set; } = true;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserSubjectAccess> UserAccesses {  get; set; }

    }
}
