using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Required for the ForeignKey attribute

namespace StudentPortal.Models
{
    public class Holiday
    {
        public int StudentId { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Month { get; set; }

        [Required]
        public int Date { get; set; }

        [Required]
        [StringLength(10)]
        public string Day { get; set; }

        [Required]
        [StringLength(100)]
        public string Occasion { get; set; }
    }
}
