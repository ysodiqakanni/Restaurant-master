using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Utility
{
    public static class FileHelper
    {
        public static string SaveImage(IFormFile file, string folderName)
        {
            string uniqueFileName = "";
            string uploadsFolder = "wwwroot/Images/" + folderName;
            Directory.CreateDirectory(uploadsFolder);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return filePath;
        }
    }
}
