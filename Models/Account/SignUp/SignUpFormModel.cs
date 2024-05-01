using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models.Account.SignUp
{
    public class SignUpFormModel
    {

        [Required]
        [FromForm(Name = "signup-contactPersonName")]
        public string UserName { get; set; } = null!;

        [Required]
        [FromForm(Name = "signup-contactPersonSurname")]
        public string UserSurName { get; set; } = null!;

        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        [FromForm(Name = "signup-contactPersonPhone")]
        public string UserPhoneNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [FromForm(Name = "signup-userAccountEmail")]
        public string UserAccountEmail { get; set; } = null!;


        [DataType(DataType.EmailAddress)]
        [FromForm(Name = "signup-contactPersonWorkEmail")]
        public string UserWorkEmail { get; set; } = null!;


        [FromForm(Name = "signup-contactPersonDescription")]
        public string UserDescription { get; set; } = null!;


        [Required]
        [MinLength(6)]
        [FromForm(Name = "signup-userPassword")]
        public string UserPassword { get; set; } = null!;


        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        [FromForm(Name = "signup-userPasswordConfirm")]
        public string UserPasswordConfirm { get; set; } = null!;


        [FromForm(Name = "signup-isChecked")]
        public bool IsChecked { get; set; }


        [FromForm(Name = "signup-isVisible")]
        public bool IsVisible { get; set; }


        [FromForm(Name = "signup-avatar")]
        public IFormFile AvatarFile { get; set; } = null!;


        [FromForm(Name = "signup-privacyConfirm")]
        public bool PrivacyConfirm { get; set; }

        public String? SavedFilename { get; set; }
    }
}
