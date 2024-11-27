using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public interface IClassService
    {
        IActionResult ShowClassTimePdf(int sem);
        IActionResult DownloadClassTimePdf(int sem);
    }
}
