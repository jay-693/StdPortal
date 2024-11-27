using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using Portal;
using System.Xml.Serialization;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
namespace PresentationLayer.Controllers.Student
{
    public class ExaminationCellController : Controller
    {
        private readonly StudentPortalDbContext Context;

        public ExaminationCellController(StudentPortalDbContext context)
        {
            Context = context;
        }
        //Display ExamTimeTable for the respective semester 
        [HttpPost]
        public IActionResult GetExamTimeTable(int semester)
        {
            // Check if the semester value is valid
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            TempData["Semester"] = semester;
            // Fetch the exam timetable from the database based on the semester
            int? id = HttpContext.Session.GetInt32("StudentId");
            var examTimeTable = Context.ExamTimeTables
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();

            if (examTimeTable == null || !examTimeTable.Any())
            {
                // Return partial view with a message if no data found
                return PartialView("_ExamTimeTable", new List<ExamTimeTable>());
            }

            return PartialView("_ExamTimeTable", examTimeTable);
        }

        public IActionResult DownloadExamTimeTable(int semester)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var examTimeTable = Context.ExamTimeTables
                .Where(e => e.Semester == semester && e.StudentId == id)
                .Select(e => new
                {
                    e.Sno,
                    e.SubjectCode,
                    e.Subject,
                    ExamDate = e.ExamDate.Date,
                    e.Timing
                })
                .ToList();

            TempData["Semester"] = semester;

            if (examTimeTable == null || !examTimeTable.Any())
            {
                return NotFound();
            }

            // Serialize the selected data to JSON
            var jsonString = System.Text.Json.JsonSerializer.Serialize(examTimeTable);

            // Generate the JSON file to download
            var fileName = $"ExamTimeTable_Semester_{semester}.json";
            var fileContent = new System.Text.UTF8Encoding().GetBytes(jsonString);
            return File(fileContent, "application/json", fileName);
        }

        [HttpPost]
        //Display Internal Marks for the respective semester 
        public IActionResult GetLabInternalMarks(int semester)
        {
            // Check if the semester value is valid
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            TempData["Semester"] = semester;
            // Fetch the exam timetable from the database based on the semester
            int? id = HttpContext.Session.GetInt32("StudentId");
            var labInternalMarks = Context.LabInternalMarks
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();

            if (labInternalMarks == null || !labInternalMarks.Any())
            {
                // Return partial view with a message if no data found
                return PartialView("_LabInternalMarks", new List<LabInternalMarks>());
            }

            return PartialView("_LabInternalMarks", labInternalMarks);
        }

        public IActionResult DownloadLabInternalMarks(int semester)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            // Project the data into a custom object for serialization
            var labInternalMarks = Context.LabInternalMarks
                .Where(e => e.Semester == semester && e.StudentId == id)
                                        .Select(e => new LabInternalDTO
                                        {
                                            Sno = e.Sno,
                                            Code = e.Code,
                                            Name = e.Name,
                                            Marks = e.Marks
                                        })
                                        .ToList();

            TempData["Semester"] = semester;

            if (labInternalMarks == null || !labInternalMarks.Any())
            {
                return NotFound();
            }

            // Serialize the filtered data into XML format
            var xmlSerializer = new XmlSerializer(typeof(List<LabInternalDTO>));  // Use the simple class
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, labInternalMarks);
                var xmlData = stringWriter.ToString();

                // Return the XML data as a downloadable file
                var fileName = $"InternalMarks_Semester_{semester}.xml";
                var fileContent = new System.Text.UTF8Encoding().GetBytes(xmlData);

                return File(fileContent, "application/xml", fileName);
            }
        }
        [HttpPost]
        //Display Semwise Attendence for the respective semester 
        public IActionResult GetSemwiseAttendence(int semester)
        {
            // Check if the semester value is valid
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            TempData["Semester"] = semester;
            int? id = HttpContext.Session.GetInt32("StudentId");
            var semwiseAttendence = Context.SemwiseAttendences
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();

            if (semwiseAttendence == null || !semwiseAttendence.Any())
            {
                // Return partial view with a message if no data found
                return PartialView("_SemwiseAttendence", new List<SemwiseAttendence>());
            }

            return PartialView("_SemwiseAttendence", semwiseAttendence);
        }

        public IActionResult DownloadSemwiseAttendence(int semester)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var semwiseAttendence = Context.SemwiseAttendences
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();
            TempData["Semester"] = semester;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add($"Semester_{semester}");
                
                // Add headers to the worksheet
                worksheet.Cells[1, 1].Value = "Month";
                worksheet.Cells[1, 2].Value = "No of Presents";
                worksheet.Cells[1, 3].Value = "No of Absents";
                worksheet.Cells[1, 4].Value = "Total Working Days";
                worksheet.Cells[1, 5].Value = "Attendence(%)";

                var averageAttendance = semwiseAttendence.Any(m => m.TotalWorkingDays > 0) ?
                semwiseAttendence.Where(m => m.TotalWorkingDays > 0).Average(m => m.AttendancePercentage) : 0;

                // Optional: Add styling for headers
                using (var range = worksheet.Cells[1, 1, 1, 5])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Fill the worksheet with data from the `semWiseAttendence` list
                int row = 2;
                foreach (var attendance in semwiseAttendence)
                {
                    worksheet.Cells[row, 1].Value = attendance.Month;
                    worksheet.Cells[row, 2].Value = attendance.NoOfPresents;
                    worksheet.Cells[row, 3].Value = attendance.NoOfAbsents;
                    worksheet.Cells[row, 4].Value = attendance.TotalWorkingDays;
                    worksheet.Cells[row, 5].Value = attendance.AttendancePercentage.ToString("F2");
                    row++;
                }
                // Add the average attendance to the bottom of the report
                worksheet.Cells[row, 4].Value = "Average Attendance";
				worksheet.Cells[row, 4].Style.Font.Bold = true;
				worksheet.Cells[row, 5].Value = averageAttendance.ToString("F2");
                // AutoFit columns for all data
                worksheet.Cells.AutoFitColumns();

                // Convert the Excel package to a byte array
                var fileContents = package.GetAsByteArray();

                // Return the Excel file for download
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"SemwiseAttendence_Semester_{semester}.xlsx");
            }
        }
        //Display Grades Details for the respective semester 
        [HttpPost]
        public IActionResult GetSemwiseGradesDetails(int semester)
        {
            // Check if the semester value is valid
            if (semester <= 0)
            {
                return BadRequest("Invalid semester value.");
            }
            TempData["Semester"] = semester;
            // Fetch the exam timetable from the database based on the semester
            int? id = HttpContext.Session.GetInt32("StudentId");
            var semwiseGradesDetails = Context.SemwiseGradesDetails
                .Where(e => e.Semester == semester && e.StudentId == id)
                .ToList();

            if (semwiseGradesDetails == null || !semwiseGradesDetails.Any())
            {
                // Return partial view with a message if no data found
                return PartialView("_SemwiseGradesDetails", new List<SemwiseGradesDetails>());
            }

            return PartialView("_SemwiseGradesDetails", semwiseGradesDetails);
        }

        public IActionResult DownloadSemwiseGradesDetails(int semester)
        {
            int? id = HttpContext.Session.GetInt32("StudentId");
            var semwiseGradesDetails = Context.SemwiseGradesDetails
                                        .Where(e => e.Semester == semester && e.StudentId == id)
                                        .ToList();
            TempData["Semester"] = semester;

            if (semwiseGradesDetails == null || !semwiseGradesDetails.Any())
            {
                return NotFound();
            }

            // Create the Word document in memory
            using (var memoryStream = new MemoryStream())
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                {
                    // Add a new main document part
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Add a title to the document
                    Paragraph title = new Paragraph(new Run(new Text($"Semwise Grades Details - Semester {semester}")));
                    body.AppendChild(title);

                    // Add data for each record
                    foreach (var item in semwiseGradesDetails)
                    {
                        Paragraph record = new Paragraph(
                            new Run(
                                new Text($"Sno: {item.Sno}, Subject Code/Lab Code: {item.SubjectCodeLabCode}, Subject Name/Lab Name: {item.Subject},Month&Year:{item.Grade},Status:{item.Status}")
                            )
                        );
                        body.AppendChild(record);
                    }

                    // Append the body to the document
                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }

                // Return the Word document as a downloadable file
                string fileName = $"SemwiseGradesDetails_Semester_{semester}.docx";
                byte[] fileContents = memoryStream.ToArray();
                return File(fileContents, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
        }

        public IActionResult SemwiseGradesDetails()
        {
            return View();
        }

        public IActionResult SemwiseAttendence()
        {
            return View();
        }
        public IActionResult LabInternalMarks()
        {
            return View();
        }
        public IActionResult ExamTimeTable()
        {
            return View();
        }
        public IActionResult RegularFee()
        {
            return View();
        }
    }
}
