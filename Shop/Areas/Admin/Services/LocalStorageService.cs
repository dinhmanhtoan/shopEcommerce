﻿using Microsoft.AspNetCore.Hosting;
using Model.Services;
using System.IO;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Services
{
   public  class LocalStorageService : IStorageService
    {
        private const string MediaRootFoler = "user-content";
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }
        public async  Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, MediaRootFoler, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }
        }
        public async Task DeleteMediaAsync(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, MediaRootFoler, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}