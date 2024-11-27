using Microsoft.EntityFrameworkCore;
using StudentPortal.Models;
namespace Portal
{
    public class StudentPortalDbContext : DbContext
    {
        public StudentPortalDbContext(DbContextOptions<StudentPortalDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BorrowedBooks>()
                 .HasOne(b => b.Student)
                 .WithMany()
                 .HasForeignKey(b => b.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BorrowedBooks>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<Holiday>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<ExamTimeTable>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ExamTimeTable>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<LabInternalMarks>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LabInternalMarks>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<SemwiseAttendence>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SemwiseAttendence>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<SemwiseGradesDetails>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SemwiseGradesDetails>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<BooksStatus>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BooksStatus>()
                .HasKey(h => h.Id);
            modelBuilder.Entity<Supply>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Supply>()
                .HasKey(h => h.Id);
        }
        // Your other DbSet properties go here
        public DbSet<SignUp> SignUps { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<ExamTimeTable> ExamTimeTables { get; set; }
        public DbSet<LabInternalMarks> LabInternalMarks { get; set; }
        public DbSet<SemwiseGradesDetails> SemwiseGradesDetails { get; set; }
        public DbSet<SemwiseAttendence> SemwiseAttendences { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
        public DbSet<BooksStatus> BooksStatus { get; set; }
        public DbSet<Supply> Supply { get; set; }
    }
}
