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
        Task<ResultModel> CreateAsync(Album album);

        /// <summary>
        /// Inserts albums by using bulk
        /// </summary>
        /// <param name="albums"></param>
        Task<ResultModel> CreateAsync(List<Album> albums);

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="album"></param>
        Task DeleteAsync(Album album);

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album"></param>
        Task UpdateAsync(Album album);

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Task<Album> GetByIdAsync(int albumId);

        /// <summary>
        /// Returns an album as Dto by AlbumId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AlbumListDto> GetByIdAsDtoAsync(int id);

        /// <summary>
        /// Returns an album lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AlbumListDto>> GetAlbumListAsync();

        /// <summary>
        /// Returns albums of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetUserAlbumsByUserIdAsync(int userId);

        /// <summary>
        /// Returns albums of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<AlbumListDto>> GetUserAlbumsWithDtoAsync(int userId);

        /// <summary>
        /// Inserts an album with images and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(AlbumCreateApi model);
    }
}
