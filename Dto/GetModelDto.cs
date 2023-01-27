using System.ComponentModel.DataAnnotations;

namespace Project.Dto
{
    public class GetModelDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public Guid MarkId { get; set; }
        public MarkDto Mark { get; set; } = null!;
    }
}