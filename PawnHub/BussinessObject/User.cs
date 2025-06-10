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

        [Required(ErrorMessage = "Telephone number is required.")]
        [Phone(ErrorMessage = "Invalid telephone number format.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), "ValidateDob")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        [Range(1, 5, ErrorMessage = "User role must be between 1 and 5.")]
        public int UserRole { get; set; }

        [Required(ErrorMessage = "CID is required.")]
        [StringLength(20, ErrorMessage = "CID cannot exceed 20 characters.")]
        public string CID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<PawnContract> PawnContracts { get; set; }
        public ICollection<Bill> Bills { get; set; }

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
