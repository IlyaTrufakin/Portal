
namespace Portal.Services.Upload
{
    public class UploadServiceV1 : IUploadService
    {
        public string SaveFromFile(IFormFile formFile)
        {
            return SaveFromFile(formFile, "wwwroot/upload/");
        }

        public string SaveFromFile(IFormFile formFile, string path)
        {
            return SaveFromFile(formFile, path, []);
        }

        public string SaveFromFile(IFormFile formFile, string path, IEnumerable<string> extensionsAllowed)
        {
            ArgumentNullException.ThrowIfNull(formFile, nameof(formFile));
            ArgumentNullException.ThrowIfNull(path, nameof(path));

            //відділяємо розширення файлу
            String ext = Path.GetExtension(formFile.FileName);

            //перевіряємо на дозволені розширення
            if (extensionsAllowed.Any() && !extensionsAllowed.Any(e => e == ext))
            {
                throw new Exception("Extension not allowed");
            }

            //генеруємо нове ім'я для файлу
            String savedName = Guid.NewGuid().ToString() + ext;


            //визначаємо місце для сбереження
            String location = Path.Combine(Directory.GetCurrentDirectory(), path, savedName);

            //зберігаємо
            using var stream = System.IO.File.OpenWrite(location);
            formFile.CopyTo(stream);
            //передаємо збереженоє ім'я в модель
            return savedName;
        }
    }
}

