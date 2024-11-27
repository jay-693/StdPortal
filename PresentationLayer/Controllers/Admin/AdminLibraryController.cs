using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal;
using StudentPortal.Models;

namespace PresentationLayer.Controllers.Admin
{
    public class AdminLibraryController : Controller
    {
        private readonly StudentPortalDbContext Context;

        public AdminLibraryController(StudentPortalDbContext context)
        {
            Context = context;
        }

        public async Task<IActionResult> BooksStatus(int pageNumber = 1, int pageSize = 10)
        {
            var books = await Context.BooksStatus
                .OrderBy(e => e.StudentId)
                .ToListAsync();

            return View(books);
        }
        [HttpGet]
        public IActionResult AddStatus()
        {
            return View();
        }

        // Add: Add status of the book
        [HttpPost]
        public async Task<IActionResult> AddStatus(BooksStatus bookStatus)
        {
            Context.BooksStatus.Add(bookStatus);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(BooksStatus));
        }

        // Edit: Modify status of the book
        [HttpGet]
        public async Task<IActionResult> EditStatus(int id)
        {
            var bookStatus = await Context.BooksStatus.FindAsync(id);
            if (bookStatus == null)
            {
                return NotFound();
            }
            return View(bookStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, BooksStatus updatedBookStatus)
        {
            if (id != updatedBookStatus.Id)
            {
                return BadRequest();
            }

            // Ensure the StudentId exists in the SignUps table
            var existingStudent = await Context.SignUps.FindAsync(updatedBookStatus.StudentId);
            if (existingStudent == null)
            {
                // If the StudentId doesn't exist, show a meaningful error
                ModelState.AddModelError(string.Empty, "The selected student does not exist.");
                return View(updatedBookStatus);
            }

            try
            {
                Context.BooksStatus.Update(updatedBookStatus);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(BooksStatus));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.BooksStatus.Any(b => b.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // Remove: Confirm removal
        public async Task<IActionResult> RemoveStatus(int id)
        {
            var bookStatus = await Context.BooksStatus.FindAsync(id);
            if (bookStatus == null)
            {
                return NotFound();
            }
            return View(bookStatus);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveStatusConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveStatusConfirmed(int id)
        {
            var bookStatus = await Context.BooksStatus.FindAsync(id);
            if (bookStatus != null)
            {
                Context.BooksStatus.Remove(bookStatus);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(BooksStatus));
        }

        public async Task<IActionResult> BorrowedBooks()
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var books = await Context.BorrowedBooks
                .OrderBy(e => e.StudentId)
                .ToListAsync();

            return View(books);
        }


        // Add: Display form
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Adds borrowed books records
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BorrowedBooks borrowedBook)
        {
            Context.BorrowedBooks.Add(borrowedBook);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(BorrowedBooks));
        }

        // Edit: Modify borrowed books records
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var borrowedBook = await Context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BorrowedBooks updatedBorrowedBook)
        {
            if (id != updatedBorrowedBook.Id)
            {
                return BadRequest();
            }

            // Ensure the StudentId exists in the SignUps table
            var existingStudent = await Context.SignUps.FindAsync(updatedBorrowedBook.StudentId);
            if (existingStudent == null)
            {
                // If the StudentId doesn't exist, show a meaningful error
                ModelState.AddModelError(string.Empty, "The selected student does not exist.");
                return View(updatedBorrowedBook);
            }

            try
            {
                Context.BorrowedBooks.Update(updatedBorrowedBook);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(BorrowedBooks));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Context.BorrowedBooks.Any(b => b.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // Remove: Confirm removal
        public async Task<IActionResult> Remove(int id)
        {
            var borrowedBook = await Context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            var borrowedBook = await Context.BorrowedBooks.FindAsync(id);
            if (borrowedBook != null)
            {
                Context.BorrowedBooks.Remove(borrowedBook);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(BorrowedBooks));
        }
    }
}
