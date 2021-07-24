using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post video service interface
    /// </summary>
    public partial interface IPostVideoService
    {
        /// <summary>
        /// Creates post videos for a post
        /// </summary>
        /// <param name="createVideoForPost"></param>
        /// <returns></returns>
        Task<ResultModel> Create(PostVideo createVideoForPost);
    }
}
