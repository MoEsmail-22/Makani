using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

namespace Graduation_Project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Db15420Context _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(Db15420Context context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(int id, string message, string Date)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var existingOrder = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && o.HouseId == id);

            if (existingOrder != null)
            {
                TempData["OrderMessage"] = "You already have a scheduled order for this property. We will contact you soon!";
            }
            else
            {
                var order = new Order
                {
                    UserId = userId.Value,
                    HouseId = id,
                    Message = message,
                    Date = Date
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                TempData["OrderMessage"] = "Your order has been placed successfully. We will contact you soon!";
            }

            return RedirectToAction("Index", "Details", new { id = id });
        }

        [HttpPost]
        public IActionResult SendOtp()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var email = user.Email;

            // Generate a 6-digit OTP
            var otp = new Random().Next(100000, 999999).ToString();

            // Store OTP and its generation time in session
            HttpContext.Session.SetString("OTP", otp);
            HttpContext.Session.SetString("OTP_Time", DateTime.Now.ToString());

            // Send email with OTP using Gmail SMTP
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("makani462852@gmail.com", "rfxt knat wusp dide\r\n"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("makani462852@gmail.com"),
                    Subject = "Your OTP for Payment",
                    Body = $"Your OTP for payment is: {otp}",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);

                return Json(new { success = true, message = "OTP sent to your email." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP email.");
                return Json(new { success = false, message = "Failed to send OTP." });
            }
        }

        [HttpPost]
        public IActionResult MockPayment(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var enteredOtp = Request.Form["Otp"].ToString();
            var storedOtp = HttpContext.Session.GetString("OTP");
            var otpTimeStr = HttpContext.Session.GetString("OTP_Time");

            // Check if OTP exists and is not expired
            if (string.IsNullOrEmpty(storedOtp) || string.IsNullOrEmpty(otpTimeStr))
            {
                TempData["OrderMessage"] = "OTP not found or expired. Please request a new one.";
                return RedirectToAction("Index", "Details", new { id = id });
            }

            var otpTime = DateTime.Parse(otpTimeStr);
            if ((DateTime.Now - otpTime).TotalMinutes > 5) // 5-minute expiration
            {
                TempData["OrderMessage"] = "OTP has expired. Please request a new one.";
                return RedirectToAction("Index", "Details", new { id = id });
            }

            if (enteredOtp != storedOtp)
            {
                TempData["OrderMessage"] = "Invalid OTP. Please try again.";
                return RedirectToAction("Index", "Details", new { id = id });
            }

            // OTP is valid, proceed with payment
            var order = new Order
            {
                UserId = userId.Value,
                HouseId = id,
                Message = null,
                Date = null
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Simulate success
            TempData["OrderMessage"] = "Payment successful! We will contact you soon.";

            var house = _context.Houses.SingleOrDefault(e => e.HouseId == id);
            house.IsApproved = false;
            _context.SaveChanges();

            // Clear OTP from session
            HttpContext.Session.Remove("OTP");
            HttpContext.Session.Remove("OTP_Time");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult OrdersList()
        {
            var orders = _context.Orders
                .Include(o => o.House)
                .Include(o => o.User)
                .Where(o => o.Date != null)
                .ToList();
            return View(orders);
        }
    }
}