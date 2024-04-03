using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgainHandbook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 20, ErrorMessage = "Display Order Must be within 1 - 20")]
        public int DisplayOrder { get; set; }
    }
}
