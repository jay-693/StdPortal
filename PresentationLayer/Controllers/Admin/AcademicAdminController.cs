using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal;
using StudentPortal.Models;

namespace PresentationLayer.Controllers.Admin
{
    public class AcademicAdminController : Controller
    {
        private readonly StudentPortalDbContext Context;
        private readonly IClassAdminRepository ClassAdminRepository;
        private readonly IBtechRepository BtechRepository;
        private readonly IMbaRepository MbaRepository;
        public AcademicAdminController(StudentPortalDbContext context, IClassAdminRepository classAdminRepository, IBtechRepository btechRepository, IMbaRepository mbaRepository)
        {
            BtechRepository = btechRepository;
            ClassAdminRepository = classAdminRepository;
            MbaRepository = mbaRepository;
            Context = context;
        }
        [HttpGet]
        public IActionResult ShowClassTimePdf(int sem)
        {
            return ClassAdminRepository.ShowPdf(sem);
        }

        [HttpGet]
        public IActionResult DownloadClassTimePdf(int sem)
        {
            return ClassAdminRepository.DownloadPdf(sem);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateClassTimePdf(int sem, IFormFile pdfFile)
        {
            var result = ClassAdminRepository.UpdatePdf(sem, pdfFile);
            if (result is OkResult)
            {
                TempData["Message"] = "PDF updated successfully.";
            }
            return RedirectToAction("ClassAdmin", "AcademicAdmin");
        }

        [HttpGet]
        public IActionResult ShowMbaPdf(int sem)
        {
            return MbaRepository.ShowPdf(sem);
        }

        [HttpGet]
        public IActionResult DownloadMbaPdf(int sem)
        {
            return MbaRepository.DownloadPdf(sem);
        }

        [HttpPost]
        public IActionResult UpdateMbaPdf(int sem, IFormFile pdfFile)
        {
            var result = MbaRepository.UpdatePdf(sem, pdfFile);
            if (result is OkResult)
            {
                TempData["Message"] = "PDF updated successfully.";
            }
            return RedirectToAction("MBAAdmin", "AcademicAdmin");
        }
        [HttpGet]
        public IActionResult ShowBtechPdf(int sem)
        {
            return BtechRepository.ShowPdf(sem);
        }

        [HttpGet]
        public IActionResult DownloadBtechPdf(int sem)
        {
            return BtechRepository.DownloadPdf(sem);
        }

        [HttpPost]
        public IActionResult UpdateBtechPdf(int sem, IFormFile pdfFile)
        {
            var result = BtechRepository.UpdatePdf(sem, pdfFile);
            if (result is OkResult)
            {
                TempData["Message"] = "PDF updated successfully.";
            }
            return RedirectToAction("BtechAdmin", "AcademicAdmin");
        }

        //Display's the Academic Calender in the iframe for the selected semester
        [HttpGet]
        public IActionResult ShowCalenderPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
            string pdfFile = GetPdfFilePath(pdfDirectory, sem);

            if (System.IO.File.Exists(pdfFile))
            {
                string fileUrl = $"/pdfs/{Path.GetFileName(pdfFile)}";
                return Redirect(fileUrl);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult DownloadCalenderPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
            string pdfFile = GetPdfFilePath(pdfDirectory, sem);

            if (System.IO.File.Exists(pdfFile))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(pdfFile);
                return File(fileBytes, "application/pdf", Path.GetFileName(pdfFile));
            }
            else
            {
                return NotFound();
            }
        }

        // Action to update the PDF for a selected semester
        [HttpPost]
        public IActionResult UpdateCalenderPdf(int sem, IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
                string pdfFilePath = GetPdfFilePath(pdfDirectory, sem);

                using (var stream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    pdfFile.CopyTo(stream);
                }
                TempData["Message"] = "PDF updated successfully.";
            }

            return RedirectToAction("AcademicAdmin");
        }

        // Helper method to get the PDF file path based on the semester
        private string GetPdfFilePath(string pdfDirectory, int sem)
        {
            return sem switch
            {
                1 => Path.Combine(pdfDirectory, "Semester1.pdf"),
                2 => Path.Combine(pdfDirectory, "Semester2.pdf"),
                3 => Path.Combine(pdfDirectory, "Semester3.pdf"),
                4 => Path.Combine(pdfDirectory, "Semester4.pdf"),
                5 => Path.Combine(pdfDirectory, "Semester5.pdf"),
                6 => Path.Combine(pdfDirectory, "Semester6.pdf"),
                7 => Path.Combine(pdfDirectory, "Semester7.pdf"),
                8 => Path.Combine(pdfDirectory, "Semester8.pdf"),
                _ => string.Empty
            };
        }
        [HttpGet]
        public IActionResult HolidayAdmin(int pageNumber = 1, int pageSize = 10)
        {
            // Fetch holidays data for pagination using the actual Holiday model
            var holidays = Context.Holidays
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(Context.Holidays.Count() / (double)pageSize);

            // Pass the holidays list to the view
            return View(holidays);
        }

        // Add new holiday (GET and POST)
        [HttpGet]
        public IActionResult AddHoliday()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHoliday(Holiday newHoliday)
        {
            if (ModelState.IsValid)
            {
                Context.Holidays.Add(newHoliday);
                Context.SaveChanges();
                return RedirectToAction("HolidayAdmin");
            }

            return View(newHoliday);
        }

        [HttpGet]
        public IActionResult EditHoliday(int id)
        {
            var holiday = Context.Holidays.FirstOrDefault(h => h.Id == id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        [HttpPost]
        public IActionResult EditHoliday(Holiday updatedHoliday)
        {
            if (ModelState.IsValid)
            {
                var existingHoliday = Context.Holidays.FirstOrDefault(h => h.Id == updatedHoliday.Id);
                if (existingHoliday != null)
                {
                    existingHoliday.Month = updatedHoliday.Month;
                    existingHoliday.Date = updatedHoliday.Date;
                    existingHoliday.Day = updatedHoliday.Day;
                    existingHoliday.Occasion = updatedHoliday.Occasion;
                    Context.SaveChanges();
                }
                return RedirectToAction("HolidayAdmin");
            }
            return View(updatedHoliday);
        }

        // Delete holiday by ID
        public IActionResult DeleteHoliday(int id)
        {
            var holiday = Context.Holidays.FirstOrDefault(h => h.Id == id);
            if (holiday != null)
            {
                Context.Holidays.Remove(holiday);
                Context.SaveChanges();
            }
            return RedirectToAction("HolidayAdmin");
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult AcademicAdmin()
        {
            return View();
        }
        public IActionResult ClassAdmin()
        {
            return View();
        }
        public IActionResult BtechAdmin()
        {
            return View();
        }
        public IActionResult MBAAdmin()
        {
            return View();
        }
    }
}
