using Portal.Models.Registration;

namespace Portal.Models.Home
{
    public class HomeModelsPageModel
    {
        public String PageTitle { get; set; } = null!;
        public RegistrationModelsViewModel? FormModel { get; set; }
    }
}
