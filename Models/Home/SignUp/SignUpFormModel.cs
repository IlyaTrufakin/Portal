using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models.Home.SignUp
{
    public class SignUpFormModel
    {

        [Required]
        [FromForm(Name = "signup-userName")]
        public string UserName { get; set; }

        [Required]
        [FromForm(Name = "signup-userSurname")]
        public string UserSurName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [FromForm(Name = "signup-userPhone")]
        public string UserPhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [FromForm(Name = "signup-userEmail")]
        public string UserEmail { get; set; }

        [Required]
        [FromForm(Name = "signup-userCountry")]
        public string UserCountry { get; set; }

        [Required]
        [FromForm(Name = "signup-userRegion")]
        public string UserRegion { get; set; }

        [Required]
        [FromForm(Name = "signup-userLocality")]
        public string UserLocality { get; set; }

        [Required]
        [FromForm(Name = "signup-userAddress1")]
        public string UserAddress1 { get; set; }

        [Required]
        [FromForm(Name = "signup-userAddress2")]
        public string UserAddress2 { get; set; }


        [Required]
        [FromForm(Name = "signup-userInteractionForm")]
        public string UserInteractionForm { get; set; }


        [Required]
        [FromForm(Name = "signup-userCompanyName")]
        public string UserCompanyName { get; set; }

        [Required]
        [MinLength(6)]
        [FromForm(Name = "signup-userPassword")]
        public string UserPassword { get; set; }

        [Required]
        [Compare("Password")]
        [FromForm(Name = "signup-userPasswordConfirm")]
        public string UserPasswordConfirm { get; set; }
    }
}
