using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Mark
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [Required(AllowEmptyStrings = true)]
        [MinLength(1)]
        [MaxLength(10)]
        public string Abbrv { get; set; } = null!;
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Manufacturer { get; set; } = null!;
        public ICollection<Model> Models { get; set; } = null!;
    }
}