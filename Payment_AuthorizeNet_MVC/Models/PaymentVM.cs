using System;
using System.ComponentModel.DataAnnotations;
using Payment_AuthorizeNet_MVC.Attributes;

namespace Payment_AuthorizeNet_MVC.Models
{
    public class PaymentVM
    {
        [Required(ErrorMessage = "Please enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [StringLength(50)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter Company")]
        [StringLength(50)]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")] //for USA only
        public decimal Zip { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Country { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Amount")]
        [RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number.")]
        [Range(typeof(Decimal), "0.01", "9999999999999999.99", ErrorMessage = "{0} must be between {1} and {2}.")]
        public decimal Amount { get; set; }

        [Required, CreditCard]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration Date is Required")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Invalid Expiration Date")]
        [ValidateExpirationDate(ErrorMessage = "Please enter correct Expiration Date")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Security Code is Required")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid Security Code")]
        public string SecurityCode { get; set; }
    }
}