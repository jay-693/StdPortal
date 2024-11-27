using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessLayer.Repositories
{
    public class ClassAdminRepository:IClassAdminRepository
    {
		//Display's Class TimeTable the  in the iframe for the selected semester
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
		// Action to update the PDF for a selected semester
		public IActionResult UpdatePdf(int sem, IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pdfs");
                string pdfFilePath = GetPdfFilePath(pdfDirectory, sem);

                using (var stream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    pdfFile.CopyTo(stream);
                }
                // You might want to return a message here or use TempData to pass a message back
                return new OkResult(); // Indicate success
            }

            return new BadRequestResult(); // Indicate failure
        }
		// Helper method to get the PDF file path based on the semester
		private string GetPdfFilePath(string pdfDirectory, int sem)
        {
            return sem switch
            {
                1 => Path.Combine(pdfDirectory, "Sem1.pdf"),
                2 => Path.Combine(pdfDirectory, "Sem2.pdf"),
                3 => Path.Combine(pdfDirectory, "Sem3.pdf"),
                4 => Path.Combine(pdfDirectory, "Sem4.pdf"),
                5 => Path.Combine(pdfDirectory, "Sem5.pdf"),
                6 => Path.Combine(pdfDirectory, "Sem6.pdf"),
                7 => Path.Combine(pdfDirectory, "Sem7.pdf"),
                8 => Path.Combine(pdfDirectory, "Sem8.pdf"),
                _ => string.Empty
            };
        }
    }
}
