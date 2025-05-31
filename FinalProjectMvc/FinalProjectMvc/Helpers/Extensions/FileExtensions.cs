using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinalProjectMvc.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string rootPath, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(rootPath, folder, fileName);

            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static void DeleteFile(this string fileName, string rootPath, string folder)
        {
            if (string.IsNullOrEmpty(fileName)) return;

            string fullPath = Path.Combine(rootPath, folder, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        public static bool IsLargerThan(this IFormFile file, int maxSizeKB)
        {
            return file.Length > maxSizeKB * 1024;
        }

        public static bool IsValidType(this IFormFile file, string expectedType)
        {
            return file.ContentType.StartsWith(expectedType + "/");
        }
        public static bool HasExtension(this IFormFile file, params string[] extensions)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            foreach (var ext in extensions)
            {
                if (fileExtension == ext.ToLower())
                    return true;
            }
            return false;
        }
    }
}
