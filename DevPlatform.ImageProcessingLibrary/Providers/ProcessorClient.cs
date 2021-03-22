using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.CreateFile;
using DevPlatform.ImageProcessingLibrary.Providers.EditFile;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;
using DevPlatform.ImageProcessingLibrary.Providers.LoadFile;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public class ProcessorClient : IFileProcessorClient
    {
        #region Fields
        private IFileProcessorCreater _fileProcessorCreater;
        private FileProcessorRequest _request;
        private ILoadFile _loadFile;
        private IEditFile _editFile;
        private ISaveFile _saveFile;
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

            _loadFile = procesorFactory.CreateLoad();
            _editFile = procesorFactory.CreateEdit();
            _saveFile = procesorFactory.CreateSave();
            _request.RequestInformation = imageProcessRequest;
        }

        public EditFileResponse ApplyEdit()
        {
            return _editFile.EditFile(_request);
        }

        public LoadFileResponse ApplyLoad()
        {
            return _loadFile.LoadFile(_request);
        }

        public SaveFileResponse ApplySave()
        {
            return _saveFile.SaveFile(_request);
        }

        #endregion
    }
}
