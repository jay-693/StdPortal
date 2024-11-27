using StudentPortal.Models;
using System.ComponentModel.DataAnnotations;

public class BorrowedBooks
{
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int Sno { get; set; } // Serial Number or Unique ID for the borrowed book

    [Required]
    [StringLength(255)]
    public string BookName { get; set; } // Name of the book

    public SignUp Student { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BorrowedDate { get; set; } // Date the book was borrowed

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; } // Date the book was returned (nullable, in case it hasn't been returned yet)
}
