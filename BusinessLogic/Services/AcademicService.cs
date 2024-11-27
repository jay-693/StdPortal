using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public class AcademicService:IAcademicService
    {
        //Display's the Academic Calender in the iframe for the selected semester
		public IActionResult ShowPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
            string pdfFile = GetPdfFilePath(pdfDirectory, sem);

            if (System.IO.File.Exists(pdfFile))
            {
                string fileUrl = $"/pdfs/{Path.GetFileName(pdfFile)}";
                return new RedirectResult(fileUrl);
            }
            else
            {
                return new NotFoundResult();
            }
        }
		public IActionResult DownloadPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
            string pdfFile = GetPdfFilePath(pdfDirectory, sem);

            if (System.IO.File.Exists(pdfFile))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(pdfFile);
                return new FileContentResult(fileBytes, "application/pdf")
                {
                    FileDownloadName = Path.GetFileName(pdfFile)
                };
            }
            else
            {
                return new NotFoundResult();
            }
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
    }
}
