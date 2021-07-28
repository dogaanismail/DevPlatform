using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration.Configs;
using DevPlatform.ImageProcessingLibrary.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Image processing service
    /// </summary>
    public partial class ImageProcessingService : IImageProcessingService
    {
        #region Fields
        private AppConfigs _appConfigs;
        private Cloudinary _cloudinary;
        private readonly IImageProcessorService _imageProcessorService;
        #endregion

        #region Ctor

        public ImageProcessingService(AppConfigs appConfigs,
            IImageProcessorService imageProcessorService)
        {
            _imageProcessorService = imageProcessorService;
            _appConfigs = appConfigs;

            Account account = new Account(
            _appConfigs.CloudinaryConfig.CloudName,
            _appConfigs.CloudinaryConfig.ApiKey,
            _appConfigs.CloudinaryConfig.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Upload an image to Cloudinary
        /// </summary>
        /// <param name="imageUploadParams"></param>
        /// <returns></returns>
        public virtual async Task<ImageUploadResult> UploadImageAsync(IFormFile image, ImageUploadParams imageUploadParams = null)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var imageUploadResult = new ImageUploadResult();

            using (var stream = image.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.Name, stream)
                };

                return imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
        }

        /// <summary>
        /// Bulk upload images to Cloudinary
        /// </summary>
        /// <param name="images"></param>
        /// <param name="imageUploadParams"></param>
        /// <returns></returns>
        public virtual async Task<List<ImageUploadResult>> UploadImageAsync(IList<IFormFile> images, IList<ImageUploadParams> imageUploadParams = null)
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));

            var uploadProcessResults = new List<ImageUploadResult>();

            foreach (var image in images)
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(image.Name, stream)
                    };

                    var imageUploadResult = await _cloudinary.UploadAsync(uploadParams);
                    uploadProcessResults.Add(imageUploadResult);
                }
            }

            return await Task.FromResult(uploadProcessResults);
        }

        /// <summary>
        /// Upload a video to Cloudinary
        /// </summary>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        public virtual async Task<VideoUploadResult> UploadVideoAsync(IFormFile video, VideoUploadParams videoUploadParams = null)
        {
            if (video == null)
                throw new ArgumentNullException(nameof(video));

            var videoUploadResult = new VideoUploadResult();

            using (var stream = video.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(video.Name, stream)
                };

                return videoUploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
        }

        /// <summary>
        /// Bulk upload videos to Cloudinary
        /// </summary>
        /// <param name="videos"></param>
        /// <param name="videoUploadParams"></param>
        /// <returns></returns>
        public virtual async Task<IList<VideoUploadResult>> UploadVideoAsync(IList<IFormFile> videos, IList<VideoUploadParams> videoUploadParams = null)
        {
            if (videos == null)
                throw new ArgumentNullException(nameof(videos));

            var uploadProcessResults = new List<VideoUploadResult>();

            foreach (var image in videos)
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(image.Name, stream)
                    };

                    var videoUploadResult = await _cloudinary.UploadAsync(uploadParams);
                    uploadProcessResults.Add(videoUploadResult);
                }
            }

            return await Task.FromResult(uploadProcessResults);
        }

        #endregion
    }
}
