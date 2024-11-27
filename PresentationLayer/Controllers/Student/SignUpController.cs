using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Portal;

namespace PresentationLayer.Controllers.Student
{
    public class SignUpController : Controller
    {
        private readonly StudentPortalDbContext Context;

        // Inject the DbContext through the constructor
        public SignUpController(StudentPortalDbContext context)
        {
            Context = context;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SignUp signUp)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        // Enable IDENTITY_INSERT for the SignUps table
                        Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT SignUps ON");

                        // Add the entity with the explicit StudentId value
                        Context.SignUps.Add(signUp);
                        Context.SaveChanges();

                        // Disable IDENTITY_INSERT after the insert operation
                        Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT SignUps OFF");

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback if there is an error
                        transaction.Rollback();
                        // Handle the error (log it, show a message, etc.)
                        throw new Exception("Error during registration.", ex);
                    }
                }
                return RedirectToAction("SignIn", "SignUp");
            }
            return View(signUp);
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public async Task<IActionResult> Login(string username, string password, string loginAs)
        {
            // Check user credentials
            var user = Context.SignUps.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                var previousLoginTime = user.LastLoginTime;

                // Store the current login time in the session
                HttpContext.Session.SetString("PreviousLoginTime", previousLoginTime?.ToString() ?? "First time login");

                // Set the current login time in the database
                user.LastLoginTime = DateTime.Now;
                Context.SignUps.Update(user);
                await Context.SaveChangesAsync();
                // Create the user's claims
                var claims = new List<Claim>
                {
                      new Claim(ClaimTypes.Name, user.Username),
                      // Set roles like Admin/Student
                      new Claim(ClaimTypes.Role, loginAs)
                 };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Session should persist across browser restarts
                    IsPersistent = true,
                    // Optional expiration for the session
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                };

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );
                string defaultImagePath = "/images/profile.jpeg";
                string profileImagePath = string.IsNullOrEmpty(user.ProfileImagePath) ? defaultImagePath : user.ProfileImagePath;
                // Set some session data
                HttpContext.Session.SetString("UserName", user.Username);
                HttpContext.Session.SetString("ProfileImage", profileImagePath);
                HttpContext.Session.SetString("Role", loginAs);
                HttpContext.Session.SetInt32("StudentId", user.StudentId);
                // Redirect to the homepage after successful login

                if (loginAs == "Admin" && user.Username == "20H51A0554")
                {
                    // Redirect to Admin dashboard or page
                    return RedirectToAction("Admin", "AcademicAdmin", new { role = loginAs });
                }
                else if (loginAs == "Student" && user.Username != "20H51A0554")
                {
                    return RedirectToAction("Index", "Home", new { username = user.Username });
                }
                else
                {
                    return @ViewBag.Error = "Invalid User.";
                }
            }

            // If login fails, show an error message
            ViewBag.Error = "Invalid username or password.";
            return View();
        }
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Remove("StudentId");
            // Redirect to SignIn page after logging out
            return RedirectToAction("HomePage", "Home");
        }
    }
}
