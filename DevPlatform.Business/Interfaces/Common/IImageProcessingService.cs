using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Common
{
    /// <summary>
    /// Image processing interface 
    /// </summary>
    public interface IImageProcessingService
    {
        /// <summary>
        /// Upload an image to Cloudinary
        /// </summary>
        /// <param name="ımageUploadParams"></param>
        /// <returns></returns>
        Task<ImageUploadResult> UploadImageAsync(IFormFile image, ImageUploadParams imageUploadParams = null);

        /// <summary>
        /// Bulk upload images to Cloudinary
        /// </summary>
        /// <param name="ımageUploadParams"></param>
        /// <returns></returns>
        Task<List<ImageUploadResult>> UploadImageAsync(IList<IFormFile> images, IList<ImageUploadParams> imageUploadParams = null);

        /// <summary>
        /// Upload a video to Cloudinary
        /// </summary>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        Task<VideoUploadResult> UploadVideoAsync(IFormFile video, VideoUploadParams videoUploadParams = null);

        /// <summary>
        /// Bulk upload videos to Cloudinary
        /// </summary>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        Task<IList<VideoUploadResult>> UploadVideoAsync(IList<IFormFile> videos, IList<VideoUploadParams> videoUploadParams = null);
    }
}
