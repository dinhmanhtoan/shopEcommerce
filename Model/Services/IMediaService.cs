using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface IMediaService
    {
        string GetMediaUrl(Media media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(Media media);
        Task SaveMediaAsync(Stream mediaBinaryStream , string fileNmae , string mimeType = null );
        Task DeleteMediaAsync(Media media);

        Task DeleteMediaAsync(string fileName);
    }
}
