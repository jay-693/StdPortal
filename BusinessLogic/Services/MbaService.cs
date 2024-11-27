using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public class MbaService:IMbaService
    {
		//Display's the Syllabus in the iframe for the selected semester
		public IActionResult ShowMbaPdf(int sem)
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
		public IActionResult DownloadMbaPdf(int sem)
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
