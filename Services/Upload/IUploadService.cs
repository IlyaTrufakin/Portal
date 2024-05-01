namespace Portal.Services.Upload
{
    public interface IUploadService
    {
        String SaveFromFile(IFormFile formFile);
        String SaveFromFile(IFormFile formFile, String path);
        String SaveFromFile(IFormFile formFile, String path, IEnumerable<string> extensionsAllowed);
    }
}
