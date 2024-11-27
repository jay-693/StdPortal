using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortal.Models
{
    public class LabInternalMarks
    {
        public int StudentId { get; set; }
        public int Sno { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  // Adjust the max length as needed
        [Column("Subject Code/Lab Code")]
        public string Code { get; set; }

		public SignUp Student { get; set; }

		[Required]
        [StringLength(100)]  // Adjust the max length as needed
        [Column("Subject Name/Lab Name")]
        public string Name { get; set; }

        [Range(0, 100)] // Assuming the marks are out of 100; adjust as needed
        [Column("Mid Marks(30)/Lab InternalMarks(40)")]
        public decimal Marks { get; set; }
        public int Semester { get; set; }
    }
}
