using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
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
        ImageUploadResult UploadImage(IFormFile image, ImageUploadParams imageUploadParams = null);

        /// <summary>
        /// Bulk upload images to Cloudinary
        /// </summary>
        /// <param name="ımageUploadParams"></param>
        /// <returns></returns>
        List<ImageUploadResult> UploadImage(IList<IFormFile> images, IList<ImageUploadParams> imageUploadParams = null);

        /// <summary>
        /// Upload a video to Cloudinary
        /// </summary>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        VideoUploadResult UploadVideo(IFormFile video, VideoUploadParams videoUploadParams = null);

        /// <summary>
        /// Bulk upload videos to Cloudinary
        /// </summary>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        IList<VideoUploadResult> UploadVideo(IList<IFormFile> videos, IList<VideoUploadParams> videoUploadParams = null);
    }
}
