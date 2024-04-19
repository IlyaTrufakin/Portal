namespace Portal.Models.Home.SignUp
{
    public class SignUpPageModel
    {
        public string PageTitle { get; set; } = null!;
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public SignUpFormModel? SignUpFormModel { get; set; }
    }
}
