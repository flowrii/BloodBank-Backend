using BloodAPI.Data;
using BloodAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloodAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private BloodContext _context;

        public AccountController(BloodContext _context)
        {
            this._context = _context;
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationModel model)
        {
            if (model.Username == "" || model.Email == "" || model.FirstName == "" || model.LastName == "" || model.Password == "" || model.Area == "" || model.BloodGroup == "")
            {
                return BadRequest("Fill all the fields");
            }
            else if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords don't match");
            }
            else
            {
                List<Donor> donors = _context.Donors.ToList();
                bool ok = true;
                for (int i = 0; i < donors.Count(); i++)
                    if (model.Username == donors.ElementAt(i).Username)
                    {
                        ok = false;
                        return BadRequest("Username already exists!");
                    }
                if (ok)
                {
                    var newUser = new Donor { Username = model.Username, Password = model.Password, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Area = model.Area, BloodGroup = model.BloodGroup };
                    _context.Donors.Add(newUser);
                    await _context.SaveChangesAsync();
                    _context.SaveChanges();

                    return Content("User registered successfully.", "text/plain", System.Text.Encoding.UTF8);
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            bool ok = false;
            string type = "";
            int id = -1;
            int donationCenterID = -1;

            if (model.Username == "" || model.Password == "")
            {
                return BadRequest("Fill all the fields");
            }
            else
            {
                List<Donor> donors = _context.Donors.ToList();
                for (int i = 0; i < donors.Count(); i++)
                    if (model.Username == donors.ElementAt(i).Username && model.Password == donors.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Donor";
                        id = donors.ElementAt(i).DonorID;
                    }
                List<Admin> admins = _context.Admins.ToList();
                for (int i = 0; i < admins.Count(); i++)
                    if (model.Username == admins.ElementAt(i).Username && model.Password == admins.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Admin";
                        id = admins.ElementAt(i).AdminID;
                    }
                List<Doctor> doctors = _context.Doctors.ToList();
                for (int i = 0; i < doctors.Count(); i++)
                    if (model.Username == doctors.ElementAt(i).Username && model.Password == doctors.ElementAt(i).Password)
                    {
                        ok = true;
                        type = "Doctor";
                        id = doctors.ElementAt(i).DoctorID;
                        donationCenterID = doctors.ElementAt(i).DonationCenterID;
                    }
                if (!ok)
                {
                    return BadRequest("Wrong username or password");
                }
                else
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    HttpContext.Session.SetString("Type", type);
                    HttpContext.Session.SetString("ID", id.ToString());

                    if (type == "Doctor")
                    {
                        HttpContext.Session.SetString("DonationCenterID", donationCenterID.ToString());
                    }

                    return Ok(new { message = "Login successful", userType = type });
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return NoContent();
        }
    }
}

