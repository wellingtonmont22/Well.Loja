using System.ComponentModel.DataAnnotations;

namespace Well.Loja.Dto
{
    public class ContactForUpdateDto
    {
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
    }
}
