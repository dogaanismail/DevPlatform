using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.EditFile
{
    public interface IEditFile
    {
        EditFileResponse EditFile(FileProcessorRequest imageProcessRequest);
    }
}
