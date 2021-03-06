﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Album;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class AlbumController : BaseApiController
    {
        #region Fields
        private readonly IAlbumService _albumService;
        private CloudinaryConfig _cloudinaryOptions;
        private Cloudinary _cloudinary;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor

        public AlbumController(IAlbumService albumService,
           CloudinaryConfig cloudinaryOptions,
           IHttpContextAccessor httpContextAccessor)
        {
            _albumService = albumService;
            _cloudinaryOptions = cloudinaryOptions;
            _httpContextAccessor = httpContextAccessor;


            Account account = new Account(
              _cloudinaryOptions.CloudName,
              _cloudinaryOptions.ApiKey,
              _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creating album
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createalbum")]
        [Authorize]
        public virtual JsonResult CreateAlbum([FromForm] AlbumCreateApi model)
        {
            if (model == null || model.Images == null || model.Images.Count == 0)
                return BadResponse(ResultModel.Error("The upload process can not be done !"));

            //could be useful if model comes as null
            var formData = _httpContextAccessor.HttpContext.Request.Form;
            var images = formData.Files;

            Album album = new Album
            {
                Name = model.Name,
                Place = model.Place,
                Date = Convert.ToDateTime(model.Date),
                Tag = model.Tag
            };

            #region ImageUploadingProcess

            foreach (var image in model.Images)
            {
                using (var stream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(image.Name, stream)
                    };

                    var imageUploadResult = _cloudinary.Upload(uploadParams);
                    if (imageUploadResult.Error != null || imageUploadResult.StatusCode != HttpStatusCode.OK)
                    {
                        return BadResponse(ResultModel.Error("The upload process can not be done !"));
                    }

                    album.AlbumImages.Add(new AlbumImage
                    {
                        ImageUrl = imageUploadResult.Url?.ToString(),
                        AlbumId = album.Id
                    });
                }

            }
            #endregion

            ResultModel albumCreateModel = _albumService.Create(album);

            if (albumCreateModel.Status)
                return OkResponse(albumCreateModel);
            else
                return BadResponse(albumCreateModel);
        }

        #endregion

        //TODO: Album list api will be added
    }
}
