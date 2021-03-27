using DevPlatform.ImageProcessingLibrary.Contract;
using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public class ImageProcessorService : IImageProcessorService
    {
        #region Fields
        private readonly IFileProcessorClient _client;

        #endregion

        #region Ctor
        public ImageProcessorService(IFileProcessorClient client)
        {
            _client = client;
        }

        #endregion

        /// <summary>
        ///  Applies a file process like edit,delete or save file
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        public IProcessResponse Apply(ImageProcessRequest imageProcessRequest)
        {
            _client.Load(imageProcessRequest);
            return _client.ApplyFileProcess();
        }
    }
}
