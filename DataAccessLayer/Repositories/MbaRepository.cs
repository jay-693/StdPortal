using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessLayer.Repositories
{
    public class MbaRepository:IMbaRepository
    {
		//Display's the Syllabus in the iframe for the selected semester
		public IActionResult ShowPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MBA");
            string pdfFile = GetPdfFilePath(pdfDirectory, sem);

            if (System.IO.File.Exists(pdfFile))
            {
                string fileUrl = $"/MBA/{Path.GetFileName(pdfFile)}";
                return new RedirectResult(fileUrl);
            }
            else
            {
                return new NotFoundResult();
            }
        }
		public IActionResult DownloadPdf(int sem)
        {
            string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MBA");
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
                string pdfDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MBA");
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
                1 => Path.Combine(pdfDirectory, "mba1.pdf"),
                2 => Path.Combine(pdfDirectory, "mba2.pdf"),
                3 => Path.Combine(pdfDirectory, "mba3.pdf"),
                4 => Path.Combine(pdfDirectory, "mba4.pdf"),
                5 => Path.Combine(pdfDirectory, "mba5.pdf"),
                _ => string.Empty
            };
        }
    }
}
