using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Graduation_Project.Models;
using Graduation_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Graduation_Project.Controllers
{
    public class AddPropertyController : Controller
    {
        private readonly Db15420Context _context;
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<AddPropertyController> _logger;

        public AddPropertyController(Db15420Context context, Cloudinary cloudinary, ILogger<AddPropertyController> logger)
        {
            _context = context;
            _cloudinary = cloudinary;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Auth");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewListing(AddPropertyViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid)
                return View("Index", model);

            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.UserId == userId.Value);
            if (owner == null)
            {
                owner = new Owner
                {
                    UserId = userId.Value,
                    Phone = model.Phone,
                    Name = "Owner Name"
                };
                _context.Owners.Add(owner);
                await _context.SaveChangesAsync();
            }

            var house = new House
            {
                Titel = model.Title,
                Location = model.Location,
                Price = model.Price,
                Description = model.Description,
                Status = model.Status,
                Type = model.PropertyType,
                OwnerId = owner.OwnerId,
                IsApproved = true
            };

            _context.Houses.Add(house);
            await _context.SaveChangesAsync();

            // Upload all normal images
            _logger.LogInformation($"Received {model.NormalImages?.Count ?? 0} images.");

            if (model.NormalImages != null)
            {
                foreach (var image in model.NormalImages)
                {
                    // Log each image's filename
                    _logger.LogInformation($"Processing image: {image.FileName}");

                    var stream = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(image.FileName, stream),
                        Folder = "house-images"
                    };
                    var result = await _cloudinary.UploadAsync(uploadParams);

                    _context.Pictures.Add(new Picture
                    {
                        Url = result.SecureUrl.ToString(),
                        HousesId = house.HouseId,
                        Is360 = false
                    });
                }
                await _context.SaveChangesAsync(); // Save all images at once
            }

            return RedirectToAction("Upload360", new { houseId = house.HouseId });
        }

        [HttpGet]
        public IActionResult Upload360(int houseId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            ViewBag.HouseId = houseId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload360(int houseId, IFormFile image360)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            if (image360 == null || image360.Length == 0)
            {
                ModelState.AddModelError("", "Please select a 360 image to upload.");
                ViewBag.HouseId = houseId;
                return View();
            }

            var stream = image360.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image360.FileName, stream),
                Folder = "house-images"
            };
            var result = await _cloudinary.UploadAsync(uploadParams);

            var picture = new Picture
            {
                Url = result.SecureUrl.ToString(),
                HousesId = houseId,
                Is360 = true
            };

            _context.Pictures.Add(picture);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
