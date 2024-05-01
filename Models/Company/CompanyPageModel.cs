namespace Portal.Models.Company
{ 
    public class CompanyPageModel
    {
        public string PageTitle { get; set; } = null!;
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }
        //public SignUpFormModel? SignUpFormModel { get; set; }
        public Dictionary<string, string> ValidationErrors { get; set; } = null!;
    }
}
