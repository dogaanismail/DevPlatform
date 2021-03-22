using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.LoadFile
{
    public interface ILoadFile
    {
        LoadFileResponse LoadFile(FileProcessorRequest imageProcessRequest);
    }
}
