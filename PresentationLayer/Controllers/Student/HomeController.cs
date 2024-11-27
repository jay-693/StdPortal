using Microsoft.AspNetCore.Mvc;
using Portal;
using Rotativa.AspNetCore;
using StudentPortal.Models;
using System.Diagnostics;

namespace PresentationLayer.Controllers.Student
{
    public class HomeController : Controller
    {
        private readonly StudentPortalDbContext Context;
        public HomeController(StudentPortalDbContext context)
        {
            Context = context;
        }
        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult RedirectToHome()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult AcademicCalender()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }
        public IActionResult ClassTimeTable()
        {
            return View();
        }
        public IActionResult ListOfHolidays()
        {
            return View();
        }
        public IActionResult TuitionFee()
        {
            return View();
        }
        public IActionResult LibraryFee()
        {
            return View();
        }
        public IActionResult HallTicket()
        {
            return View();
        }
        public IActionResult PenaltyFee()
        {
            return View();
        }
        public IActionResult TransportFee()
        {
            return View();
        }
        public IActionResult Index(string username)
        {
            // Check if username is provided
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.Username = username;
            }
            return View();
        }

        // Action to show paginated list of holidays
        public IActionResult HolidayList(int pageNumber = 1, int pageSize = 10)
        {
            // Fetch holidays with pagination
            var holidays = Context.Holidays
                .Select(h => new { h.Month, h.Date, h.Day, h.Occasion })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pass the current page number and total count to the view
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(Context.Holidays.Count() / (double)pageSize);

            return View(holidays);
        }

        // Action to show all holidays in PDF (without pagination)
        public IActionResult HolidayPdf()
        {
            var holidays = Context.Holidays
                .Select(h => new { h.Month, h.Date, h.Day, h.Occasion })
                .ToList();

            return View(holidays);
        }

        //Download pdf using Rotativa
        [HttpGet]
        public IActionResult DownloadHolidaysPdf()
        {
            // Fetch the holiday data from the database
            var holidays = Context.Holidays.ToList();

            // Return the view as a PDF using Rotativa
            return new ViewAsPdf("HolidayPdf", holidays)
            {
                FileName = "Holidays.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10)
            };
        }
        [HttpPost]
        public IActionResult GetSupply(int semester)
        {
            // Check if the semester value is valid
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            TempData["Semester"] = semester;
            // Fetch the exam timetable from the database based on the semester
            int? id = HttpContext.Session.GetInt32("StudentId");
            var supplies = Context.Supply
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();
            if (supplies == null || !supplies.Any())
            {
                // Return partial view with a message if no data found
                return PartialView("_DueSubjects", new List<Supply>());
            }

            return PartialView("_DueSubjects", supplies);
        }
        //Download pdf using Rotativa
        public IActionResult DownloadSupply(int semester)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var supplies = Context.Supply
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();
            TempData["Semester"] = semester;

            // Return the view as a PDF using Rotativa
            return new ViewAsPdf("SupplyPdf", supplies)
            {
                FileName = $"DueSubjects_Semester_{semester}.pdf"
            };
        }
        public IActionResult SupplyList()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
