using System.ComponentModel.DataAnnotations;

namespace Project.Dto
{
    public class CreateModelDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public Guid MarkId { get; set; }
    }
}