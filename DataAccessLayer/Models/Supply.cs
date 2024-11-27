using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models
{
	public class Supply
	{
		public int StudentId { get; set; }
        [Key]
        public int Id { get; set; }
        public SignUp Student { get; set; }
		public int Sno { get; set; }
		public string SubjectName { get; set; }
        public int Semester { get; set; }

    }
}
