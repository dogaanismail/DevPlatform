using DevPlatform.Core.Domain.Album;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.AlbumDto;
using DevPlatform.Domain.ServiceResponseModels.AlbumService;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Album service interface
    /// </summary>
    public partial interface IAlbumService
    {
        /// <summary>
        /// Inserts an album
        /// </summary>
        /// <param name="album"></param>
        ResultModel Create(Album album);

        /// <summary>
        /// Inserts albums by using bulk
        /// </summary>
        /// <param name="albums"></param>
        ResultModel Create(List<Album> albums);

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="album"></param>
        void Delete(Album album);

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album"></param>
        void Update(Album album);

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Album GetById(int albumId);

        /// <summary>
        /// Returns an album as Dto by AlbumId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AlbumListDto GetByIdAsDto(int id);

        /// <summary>
        /// Returns an album lists
        /// </summary>
        /// <returns></returns>
        IEnumerable<AlbumListDto> GetAlbumList();

        /// <summary>
        /// Returns albums of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Album> GetUserAlbumsByUserId(int userId);

        /// <summary>
        /// Returns albums of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<AlbumListDto> GetUserAlbumsWithDto(int userId);

        /// <summary>
        /// Inserts an album with images and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<CreateResponse> Create(AlbumCreateApi model);
    }
}
