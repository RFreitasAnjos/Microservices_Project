using System.ComponentModel.DataAnnotations;

namespace Educational_Victoria.Models
{
    public class UserSubjectAccess
    {
        [Key]
        public int AccessId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }
    }

}
