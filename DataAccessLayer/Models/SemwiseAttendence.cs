using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortal.Models
{
    public class SemwiseAttendence
    {
        public int StudentId { get; set; }
        public int Semester { get; set; }
        public SignUp Student { get; set; }
        [Key]
        public int Id { get; set; }
        public string Month { get; set; }
        public int NoOfPresents { get; set; }
        public int NoOfAbsents { get; set; }
        public int TotalWorkingDays { get; set; }

        // Calculated property for Attendance percentage
        [NotMapped]
        public decimal AttendancePercentage
        {
            get
            {
                return TotalWorkingDays > 0 ? ((decimal)NoOfPresents / TotalWorkingDays) * 100 : 0;
            }
        }
        //public decimal Average { get; set; }
    }
}
