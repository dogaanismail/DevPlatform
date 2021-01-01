using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Album;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.AlbumDto;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Album service
    /// </summary>
    public class AlbumService : IAlbumService
    {
        #region Fields
        private readonly IRepository<Album> _albumRepository;
        #endregion

        #region Ctor
        public AlbumService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Inserts an album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public ResultModel Create(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            _albumRepository.Insert(album);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts albums by using bulk
        /// </summary>
        /// <param name="albums"></param>
        /// <returns></returns>
        public ResultModel Create(List<Album> albums)
        {
            if (albums == null)
                throw new ArgumentNullException(nameof(albums));

            _albumRepository.Insert(albums);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="album"></param>
        public void Delete(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            _albumRepository.Delete(album);
        }

        public IEnumerable<AlbumListDto> GetAlbumList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public Album GetById(int albumId)
        {
            if (albumId == 0)
                return null;

            return _albumRepository.GetById(albumId);
        }

        public AlbumListDto GetByIdAsDto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Album> GetUserAlbumsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlbumListDto> GetUserAlbumsWithDto(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album"></param>
        public void Update(Album album)
        {
            if (album == null)
                throw new ArgumentNullException(nameof(album));

            _albumRepository.Update(album);
        }

        #endregion
    }
}
