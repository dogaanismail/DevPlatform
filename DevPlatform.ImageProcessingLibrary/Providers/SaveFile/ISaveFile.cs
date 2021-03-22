using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.CreateFile
{
    public interface ISaveFile
    {
        SaveFileResponse SaveFile(FileProcessorRequest imageProcessRequest);
    }
}
