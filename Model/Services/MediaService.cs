using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class MediaService : IMediaService
    {
        private readonly shopContext _context;
        private readonly IStorageService _storageService;
        public MediaService(shopContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public Task DeleteMediaAsync(Media media)
        {
            _context.Medias.Remove(media);
            return DeleteMediaAsync(media.FileName);
        }

        public Task DeleteMediaAsync(string fileName)
        {
            return _storageService.DeleteMediaAsync(fileName);
        }

        public string GetMediaUrl(Media media)
        {
            if (media == null)
            {
                return GetMediaUrl("no-image.png");
            }

            return GetMediaUrl(media.FileName);
        }

        public string GetMediaUrl(string fileName)
        {
            return _storageService.GetMediaUrl(fileName);
        }

        public string GetThumbnailUrl(Media media)
        {
            return GetMediaUrl(media);
        }

        public Task SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            return _storageService.SaveMediaAsync(mediaBinaryStream, fileName, mimeType);
        }
    }
}
