using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Album;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.AlbumDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.AlbumService;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Album service
    /// </summary>
    public partial class AlbumService : ServiceExecute, IAlbumService
    {
        #region Fields
        private readonly IRepository<Album> _albumRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor
        public AlbumService(IRepository<Album> albumRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageProcessingService imageProcessingService,
            ILogService logService)
        {
            _albumRepository = albumRepository;
            _httpContextAccessor = httpContextAccessor;
            _imageProcessingService = imageProcessingService;
            _logService = logService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Inserts an album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            await _albumRepository.InsertAsync(album);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts albums by using bulk
        /// </summary>
        /// <param name="albums"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(List<Album> albums)
        {
            if (albums == null)
                throw new ArgumentNullException(nameof(albums));

            await _albumRepository.InsertAsync(albums);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts an album with images and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<ServiceResponse<CreateResponse>> CreateAsync(AlbumCreateApi model)
        {
            if (model == null || model.Images == null || model.Images.Count == 0)
                return ServiceResponse((CreateResponse)null, new List<string> { "The upload process can not be done !" });

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                //could be useful if model comes as null
                var formData = _httpContextAccessor.HttpContext.Request.Form;
                var images = formData.Files;

                Album album = new()
                {
                    Name = model.Name,
                    Place = model.Place,
                    Date = Convert.ToDateTime(model.Date),
                    Tag = model.Tag
                };

                var uploadResults = await _imageProcessingService.UploadImageAsync(model.Images);

                if (uploadResults.Any(x => x.Error != null || x.StatusCode != System.Net.HttpStatusCode.OK))
                    return ServiceResponse((CreateResponse)null, new List<string>
                    (
                        uploadResults.Select(x => x.Error.Message).ToList()
                    ));

                foreach (var result in uploadResults)
                {
                    album.AlbumImages.Add(new AlbumImage
                    {
                        ImageUrl = result.Url?.ToString(),
                        AlbumId = album.Id
                    });
                }

                ResultModel createResult = await CreateAsync(album);

                if (!createResult.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { createResult.Message });

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new CreateResponse
                {
                    Succeeded = true
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"AlbumService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="album"></param>
        public virtual async Task DeleteAsync(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            await _albumRepository.DeleteAsync(album);
        }

        public virtual async Task<List<AlbumListDto>> GetAlbumListAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public virtual async Task<Album> GetByIdAsync(int albumId)
        {
            if (albumId == 0)
                return null;

            return await _albumRepository.GetByIdAsync(albumId);
        }

        public virtual async Task<AlbumListDto> GetByIdAsDtoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<Album>> GetUserAlbumsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<AlbumListDto>> GetUserAlbumsWithDtoAsync(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album"></param>
        public virtual async Task UpdateAsync(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            await _albumRepository.UpdateAsync(album);
        }

        #endregion
    }
}
