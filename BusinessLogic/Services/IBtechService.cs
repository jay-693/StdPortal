using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public interface IBtechService
    {
        IActionResult ShowBtechPdf(int sem);
        IActionResult DownloadBtechPdf(int sem);
    }
}
