using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Real name is required.")]
        [StringLength(100, ErrorMessage = "Real name cannot exceed 100 characters.")]
        public string UserRealName { get; set; }

        [StringLength(20, ErrorMessage = "Telephone number cannot exceed 20 characters.")]
        public string? Telephone { get; set; }

        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), "ValidateDob")]
        public DateTime? Dob { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        [Range(1, 5, ErrorMessage = "User role must be between 1 and 5.")]
        public int UserRole { get; set; }

        [StringLength(20, ErrorMessage = "CID cannot exceed 20 characters.")]
        public string? CID { get; set; }

        // Password không bắt buộc cho Google login
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? Password { get; set; }

        // Thêm các trường cho Google login
        public string? GoogleId { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsGoogleAccount { get; set; } = false;

        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<PawnContract> PawnContracts { get; set; } = new List<PawnContract>();
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        // Custom validation for Dob to ensure the user is at least 18 years old.
        public static ValidationResult ValidateDob(DateTime? dob, ValidationContext context)
        {
            if (dob == null)
                return ValidationResult.Success;

            var age = DateTime.Now.Year - dob.Value.Year;
            if (dob.Value.AddYears(age) > DateTime.Now)
                age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("User must be at least 18 years old.");
        }
    }
}
