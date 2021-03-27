using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.ImageSharp
{
    /// <summary>
    /// ImageSharpProcess class implementations
    /// </summary>
    public class ImageSharpProcess : IFileProcess
    {
        #region Methods
        public ImageProcessResponse Apply(FileProcessorRequest imageProcessRequest)
        {
            return imageProcessRequest.Helper.CreateFileProcessRequest(imageProcessRequest.RequestInformation);
        }

        #endregion
    }
}
