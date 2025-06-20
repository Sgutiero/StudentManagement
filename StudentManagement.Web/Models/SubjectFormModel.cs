using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Web.Models
{
    public class SubjectFormModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int Credits { get; set; }
    }
}
