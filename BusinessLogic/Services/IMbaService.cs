using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public interface IMbaService
    {
        IActionResult ShowMbaPdf(int sem);
        IActionResult DownloadMbaPdf(int sem);
    }
}
