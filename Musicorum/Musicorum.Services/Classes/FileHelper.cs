using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;

namespace Musicorum.Services.Classes
{
    public class FileHelper
    {
        public static byte[] FileAsBytes(IFormFile file)
        {
            if(file == null)
            {
                return null;
            }

            byte[] fileAsBytes;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileAsBytes = memoryStream.ToArray();
            };

            return fileAsBytes;
        }

        public static IFormFile BytesToFile(byte[] fileBytes)
        {
            if (fileBytes == null)
            {
                return null;
            }

            IFormFile file = null;

            using (var memoryStream = new MemoryStream(fileBytes))
            {
                file = new FormFile(memoryStream, 0, fileBytes.Length, "name", "fileName");
            };

            return file;
        }
    }
}
