using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Sv.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [Required]
        [StringLength(500, MinimumLength =5)]
        public string? Content{ get; set; }
        public string ApplicationUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ValidateNever]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
