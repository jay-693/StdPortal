using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Portal;

namespace PresentationLayer.Controllers.Student
{
    public class CourseController : Controller
    {
        private readonly StudentPortalDbContext Context;
        private readonly IBtechService BtechService;
        private readonly IMbaService MbaService;
        public CourseController(StudentPortalDbContext context,IBtechService btechService, IMbaService mbaService)
        {
            Context = context;
            BtechService = btechService;
            MbaService = mbaService;
        }
        // Display Btech Syllabus in the iframe
        [HttpGet]
        public IActionResult ShowBtechPdf(int sem)
        {
            return BtechService.ShowBtechPdf(sem);
        }

        [HttpGet]
        public IActionResult DownloadBtechPdf(int sem)
        {
            return BtechService.DownloadBtechPdf(sem);
        }
        // Display Mba Syllabus in the iframe
        [HttpGet]
        public IActionResult ShowMbaPdf(int sem)
        {
            return MbaService.ShowMbaPdf(sem);
        }

        [HttpGet]
        public IActionResult DownloadMbaPdf(int sem)
        {
            return MbaService.DownloadMbaPdf(sem);
        }
        public IActionResult Btech()
        {
            return View();
        }
        public IActionResult Mba()
        {
            return View();
        }
        // Action to show paginated list of holidays
        public IActionResult GetStatus(int pageNumber = 1, int pageSize = 10)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var totalRecords = Context.BooksStatus.Count(e => e.StudentId == id);
            var books = Context.BooksStatus
                .Where(e => e.StudentId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var totalPenalty = books.Sum(book => book.Penalty);
            HttpContext.Session.SetInt32("TotalPenalty", totalPenalty);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            ViewBag.TotalRecords = totalRecords;
            return View(books);
        }
        public IActionResult GetBooks(int pageNumber = 1, int pageSize = 10)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var totalRecords = Context.BorrowedBooks.Count(e => e.StudentId == id);

            var books = Context.BorrowedBooks
                .Where(e => e.StudentId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            ViewBag.TotalRecords = totalRecords;

            return View(books);
        }
    }
}

