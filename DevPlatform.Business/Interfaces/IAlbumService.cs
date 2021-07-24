using DevPlatform.Core.Domain.Album;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.AlbumDto;
using DevPlatform.Domain.ServiceResponseModels.AlbumService;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<ResultModel> Create(Album album);

        /// <summary>
        /// Inserts albums by using bulk
        /// </summary>
        /// <param name="albums"></param>
        Task<ResultModel> Create(List<Album> albums);

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="album"></param>
        Task Delete(Album album);

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album"></param>
        Task Update(Album album);

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<Album> GetById(int albumId);

        /// <summary>
        /// Returns an album as Dto by AlbumId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AlbumListDto> GetByIdAsDto(int id);

        /// <summary>
        /// Returns an album lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AlbumListDto>> GetAlbumList();

        /// <summary>
        /// Returns albums of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetUserAlbumsByUserId(int userId);

        /// <summary>
        /// Returns albums of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<AlbumListDto>> GetUserAlbumsWithDto(int userId);

        /// <summary>
        /// Inserts an album with images and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(AlbumCreateApi model);
    }
}
