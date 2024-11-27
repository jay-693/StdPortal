using Microsoft.AspNetCore.Mvc;
using Portal;
using Rotativa.AspNetCore;
using StudentPortal.Models;
namespace PresentationLayer.Controllers.Admin
{
    public class AdminExaminationCellController : Controller
    {
        private readonly StudentPortalDbContext Context;

        public AdminExaminationCellController(StudentPortalDbContext context)
        {
            Context = context;
        }
        [HttpPost]
        public IActionResult GetExamTimeTable(int semester)
        {
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }

            var semwiseGradesDetails = Context.ExamTimeTables
                 .Where(e => e.Semester == semester).OrderBy(e => e.StudentId)
                 .ToList();

            if (semwiseGradesDetails == null || !semwiseGradesDetails.Any())
            {
                return PartialView("_AdminExamTimeTable", new List<ExamTimeTable>());
            }

            return PartialView("_AdminExamTimeTable", semwiseGradesDetails); // Render this into the div
        }
        [HttpGet]
        public IActionResult AddExamTimeTable()
        {
            return View();
        }

        // Add Post Method to save the new grade details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExamTimeTable(ExamTimeTable model)
        {
            // Add the new record to the database
            Context.ExamTimeTables.Add(model);

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminExamTimeTable));
        }

        // Edit Get Method to display the Edit form
        [HttpGet]
        public IActionResult EditExamTimeTable(int id)
        {
            var record = Context.ExamTimeTables.FirstOrDefault(e => e.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // Edit Post Method to save the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExamTimeTable(ExamTimeTable model)
        {
            Context.ExamTimeTables.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminExamTimeTable));
        }

        public async Task<IActionResult> RemoveExamTimeTable(int id)
        {
            var borrowedBook = await Context.ExamTimeTables.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveExamTimeTableConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveExamTimeTableConfirmed(int id)
        {
            var borrowedBook = await Context.ExamTimeTables.FindAsync(id);
            if (borrowedBook != null)
            {
                Context.ExamTimeTables.Remove(borrowedBook);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminExamTimeTable));
        }
        public IActionResult AdminExamTimeTable()
        {
            // Fetch all records for displaying
            var semwiseGradesDetails = Context.ExamTimeTables.OrderBy(e => e.Sno).ToList();
            return View(semwiseGradesDetails);
        }
        [HttpPost]
        public IActionResult GetLabInternal(int semester)
        {
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            var semwiseGradesDetails = Context.LabInternalMarks
              .Where(e => e.Semester == semester).OrderBy(e => e.StudentId)
              .ToList();

            if (semwiseGradesDetails == null || !semwiseGradesDetails.Any())
            {
                return PartialView("_AdminLabInternal", new List<LabInternalMarks>());
            }

            return PartialView("_AdminLabInternal", semwiseGradesDetails); // Render this into the div
        }
        [HttpGet]
        public IActionResult AddLabInternal()
        {
            return View();
        }

        // Add Post Method to save the new grade details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLabInternal(LabInternalMarks model)
        {
            Context.LabInternalMarks.Add(model);

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminLab));
        }

        // Edit Get Method to display the Edit form
        [HttpGet]
        public IActionResult EditLabInternal(int id)
        {
            var record = Context.LabInternalMarks.FirstOrDefault(e => e.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // Edit Post Method to save the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLabInternal(LabInternalMarks model)
        {
            Context.LabInternalMarks.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminLab));
        }

        public async Task<IActionResult> RemoveLabInternal(int id)
        {
            var borrowedBook = await Context.LabInternalMarks.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveLabInternalConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLabInternalConfirmed(int id)
        {
            var borrowedBook = await Context.LabInternalMarks.FindAsync(id);
            if (borrowedBook != null)
            {
                Context.LabInternalMarks.Remove(borrowedBook);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminLab));
        }
        public IActionResult AdminLab()
        {
            // Fetch all records for displaying
            var semwiseGradesDetails = Context.LabInternalMarks.OrderBy(e => e.Sno).ToList();
            return View(semwiseGradesDetails);
        }

        [HttpPost]
        public IActionResult GetSemwiseGradesDetails(int semester)
        {
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            var semwiseGradesDetails = Context.SemwiseGradesDetails
              .Where(e => e.Semester == semester).OrderBy(e => e.StudentId)
              .ToList();

            if (semwiseGradesDetails == null || !semwiseGradesDetails.Any())
            {
                return PartialView("_AdminSemwiseGradesDetails", new List<SemwiseGradesDetails>());
            }

            return PartialView("_AdminSemwiseGradesDetails", semwiseGradesDetails); // Render this into the div
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Add Post Method to save the new grade details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSemwiseGrades(SemwiseGradesDetails model)
        {
            Context.SemwiseGradesDetails.Add(model);

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminSemWiseGradesDetails));
        }

        // Edit Get Method to display the Edit form
        [HttpGet]
        public IActionResult EditSemwiseGrades(int id)
        {
            var record = Context.SemwiseGradesDetails.FirstOrDefault(e => e.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // Edit Post Method to save the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSemwiseGrades(SemwiseGradesDetails model)
        {
            Context.SemwiseGradesDetails.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminSemWiseGradesDetails));
        }


        public async Task<IActionResult> RemoveSemwiseGrades(int id)
        {
            var borrowedBook = await Context.SemwiseGradesDetails.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveSemwiseGradesConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSemwiseGradesConfirmed(int id)
        {
            var borrowedBook = await Context.SemwiseGradesDetails.FindAsync(id);
            if (borrowedBook != null)
            {
                Context.SemwiseGradesDetails.Remove(borrowedBook);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminSemWiseGradesDetails));
        }
        public IActionResult AdminSemWiseGradesDetails()
        {
            // Fetch all records for displaying
            var semwiseGradesDetails = Context.SemwiseGradesDetails.OrderBy(e => e.Sno).ToList();
            return View(semwiseGradesDetails);
        }


        public IActionResult DownloadSemwiseGradesDetails(int semester)
        {
            var semwiseGradesDetails = Context.SemwiseGradesDetails
                .Where(e => e.Semester == semester)
                .OrderBy(e => e.Sno)
                .ToList();

            return new ViewAsPdf("semwiseGradesDetailsPdf", semwiseGradesDetails)
            {
                FileName = $"SemwiseGradesDetails_Semester_{semester}.pdf"
            };
        }

        [HttpPost]
        public IActionResult GetSupply(int semester)
        {
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            var supplyDetails = Context.Supply
              .Where(e => e.Semester == semester).OrderBy(e => e.StudentId)
              .ToList();

            if (supplyDetails == null || !supplyDetails.Any())
            {
                return PartialView("_AdminSupply", new List<Supply>());
            }

            return PartialView("_AdminSupply", supplyDetails); // Render this into the div
        }
        [HttpGet]
        public IActionResult AddSupplyDetails()
        {
            return View();
        }

        // Add Post Method to save the new grade details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSupplyDetails(Supply model)
        {
            // Add the new record to the database
            Context.Supply.Add(model);

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminSupply));
        }

        // Edit Get Method to display the Edit form
        [HttpGet]
        public IActionResult EditSupplyDetails(int id)
        {
            var record = Context.Supply.FirstOrDefault(e => e.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // Edit Post Method to save the changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSupplyDetails(Supply model)
        {
            Context.Supply.Update(model);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminSupply));
        }

        public async Task<IActionResult> RemoveSupplyDetails(int id)
        {
            var borrowedBook = await Context.Supply.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            return View(borrowedBook);
        }

        // Remove: Handle form submission
        [HttpPost, ActionName("RemoveSupplyDetailsConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSupplyDetailsConfirmed(int id)
        {
            var borrowedBook = await Context.Supply.FindAsync(id);
            if (borrowedBook != null)
            {
                Context.Supply.Remove(borrowedBook);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminSupply));
        }
        public IActionResult AdminSupply()
        {
            // Fetch all records for displaying
            var semwiseGradesDetails = Context.Supply.OrderBy(e => e.Sno).ToList();
            return View(semwiseGradesDetails);
        }

    }
}
