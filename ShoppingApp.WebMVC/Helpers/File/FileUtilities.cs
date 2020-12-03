using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoppingApp.Core.Utilities.Extensions;

namespace ShoppingApp.WebMVC.Helpers.File
{
    public static class FileUtilities
    {
        public static String Base64ToImage(string base64string, IWebHostEnvironment _env)
        {
            try
            {
                base64string = base64string.Split(",")[1];
                var base64array = Convert.FromBase64String(base64string);

                string fileName = Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.Millisecond + ".jpg";

                var filePath = Path.Combine($"{_env.WebRootPath}/images/{fileName}");
                System.IO.File.WriteAllBytes(filePath, base64array);
                return fileName;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public static String ImageUpload(string name, IFormFile pictureFile, IWebHostEnvironment _env)
        {
            string wwwroot = _env.WebRootPath;
            
            string fileExtension = Path.GetExtension(pictureFile.FileName);

            var dateTime = DateTime.Now;
            string fileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderScore()}.jpg";

            var path = Path.Combine($"{wwwroot}/profile", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                pictureFile.CopyTo(stream);
            }
            return fileName;
        }

        public static bool DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
