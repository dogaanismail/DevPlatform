using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    /// <summary>
    /// ProcessorClient class implementations
    /// </summary>
    public class ProcessorClient : IFileProcessorClient
    {
        #region Fields
        private IFileProcessorCreater _fileProcessorCreater;
        private FileProcessorRequest _request;
        private IFileProcess _fileProcess;

        #endregion

        #region Ctor

        public ProcessorClient(IFileProcessorCreater fileProcessorCreater)
        {
            _fileProcessorCreater = fileProcessorCreater;
        }

        #endregion

        #region Methods
        public void Load(ImageProcessRequest imageProcessRequest)
        {
            var procesorFactory = _fileProcessorCreater.ProcessorFactory(imageProcessRequest.SystemInformation.LibraryType);
            _request = new FileProcessorRequest
            {
                Helper = procesorFactory.CreateRequest(imageProcessRequest.SystemInformation.PostType)
            };

            _fileProcess = procesorFactory.ApplyFileProcess();

            _request.RequestInformation = imageProcessRequest;
        }

        /// <summary>
        /// Applies a file process like edit,delete or save file
        /// </summary>
        /// <returns></returns>
        public ImageProcessResponse ApplyFileProcess()
        {
            return _fileProcess.Apply(_request);
        }

        #endregion
    }
}
