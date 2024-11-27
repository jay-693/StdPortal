using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models
{
	public class BooksStatus
	{
		[Key]
		public int Id { get; set; }
		public int StudentId { get; set; }
		public int Sno { get; set; }
		public string BookName { get; set; }
		public string Status { get; set; }
		public SignUp Student { get; set; }
		public int Penalty { get; set; }
	}
}
