using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Model
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public Guid MarkId { get; set; }
        public Mark Mark { get; set; } = null!;
    }
}