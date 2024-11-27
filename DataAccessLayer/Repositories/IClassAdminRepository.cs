﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessLayer.Repositories
{
    public interface IClassAdminRepository
    {
        IActionResult ShowPdf(int sem);
        IActionResult DownloadPdf(int sem);
        IActionResult UpdatePdf(int sem, IFormFile pdfFile);
    }
}