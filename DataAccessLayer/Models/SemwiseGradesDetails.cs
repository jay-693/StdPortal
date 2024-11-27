using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StudentPortal.Models
{
    public class SemwiseGradesDetails
    {
        public int StudentId { get; set; }

        [Key]
        public int Id { get; set; }
        public int Sno { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Subject code/Lab Code")]  // Custom column name with space and slash
        public string SubjectCodeLabCode { get; set; }

		public SignUp Student { get; set; }

		[Required]
        [StringLength(100)]
        [Column("Subject Name/Lab Name")]  // Custom column name with space and slash
        public string Subject { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Month&year")]  // Custom column name with special character &
        public string MonthAndYear { get; set; }

        [Required]
        [StringLength(5)]
        public string Grade { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        public int Semester { get; set; }
    }
}
