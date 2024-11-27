using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers.Student
{
    public class AcademicCalendarController : Controller
    {
        private readonly IAcademicService AcademicService;
        private readonly IClassService ClassService;
        public AcademicCalendarController(IAcademicService academicService, IClassService classService)
        {
            AcademicService = academicService;
            ClassService = classService;
        }
        // Display Academic Calender in the iframe
        [HttpGet]
        public IActionResult ShowPdf(int sem)
        {
            return AcademicService.ShowPdf(sem);
        }
        [HttpGet]
        public IActionResult DownloadPdf(int sem)
        {
            return AcademicService.DownloadPdf(sem);
        }
        //Display's the Class TimeTable in the iframe for the selected semester
        [HttpGet]
        public IActionResult ShowClassTimePdf(int sem)
        {
            return ClassService.ShowClassTimePdf(sem);
        }
        [HttpGet]
        public IActionResult DownloadClassTimePdf(int sem)
        {
            return ClassService.DownloadClassTimePdf(sem);
        }

        //Display's the Hallticket in the iframe for the selected semester
        [HttpGet]
        public IActionResult GetHallPdf(int sem, string monthYear)
        {
            // Define the path where your PDFs are stored
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "HallTickets");
            string pdfFile = string.Empty;

            // Select the PDF based on the semester and month&year
            if (sem == 1 && monthYear == "April 2024")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket1.pdf");
            }
            else if (sem == 1 && monthYear == "November 2024")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket2.pdf");
            }
            else if (sem == 2 && monthYear == "April 2025")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket3.pdf");
            }
            // Add similar conditions for other semester and month/year combinations
            else
            {
                return NotFound();
            }

            if (System.IO.File.Exists(pdfFile))
            {
                // Return the URL for the PDF file to be embedded in the page
                string fileUrl = $"/HallTickets/{Path.GetFileName(pdfFile)}";
                return Redirect(fileUrl);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult DownloadHallPdf(int sem, string monthYear)
        {
            // Define the path where your PDFs are stored
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "HallTickets");
            string pdfFile = string.Empty;

            // Select the PDF based on the semester and month/year
            if (sem == 1 && monthYear == "April 2024")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket1.pdf");
            }
            else if (sem == 1 && monthYear == "November 2024")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket2.pdf");
            }
            else if (sem == 2 && monthYear == "April 2025")
            {
                pdfFile = Path.Combine(pdfDirectory, "hallticket3.pdf");
            }
            // Add similar conditions for other semester and month/year combinations
            else
            {
                return NotFound();
            }

            if (System.IO.File.Exists(pdfFile))
            {
                // Send the PDF file as a response to be downloaded
                byte[] fileBytes = System.IO.File.ReadAllBytes(pdfFile);
                return File(fileBytes, "application/pdf", Path.GetFileName(pdfFile));
            }
            else
            {
                return NotFound();
            }
        }

    }
}