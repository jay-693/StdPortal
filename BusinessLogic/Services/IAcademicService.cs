using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public interface IAcademicService
    {
        IActionResult ShowPdf(int sem);
        IActionResult DownloadPdf(int sem);
    }
}
