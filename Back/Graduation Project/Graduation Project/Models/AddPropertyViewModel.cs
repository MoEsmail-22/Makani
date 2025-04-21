using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.ViewModels
{
    public class AddPropertyViewModel
    {
        public string Title { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; } // Rent or Sale

        [Required]
        public string PropertyType { get; set; }

        [Required]
        public string Phone { get; set; }

        public List<IFormFile> NormalImages { get; set; } = new();
        public List<IFormFile> Images360 { get; set; } = new(); // for future use
    }
}
