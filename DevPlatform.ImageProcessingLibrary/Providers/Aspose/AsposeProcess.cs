using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.Aspose
{
    /// <summary>
    /// Aspose process class implementations
    /// </summary>
    public class AsposeProcess : IFileProcess
    {
        #region Methods

        /// <summary>
        /// Applies a file process
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        public ImageProcessResponse Apply(FileProcessorRequest imageProcessRequest)
        {
            return imageProcessRequest.Helper.CreateFileProcessRequest(imageProcessRequest.RequestInformation);
        }

        #endregion
    }
}
