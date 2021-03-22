using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Contract
{
    public interface IImageProcessorService
    {
        IProcessResponse LoadFile(ImageProcessRequest imageProcessRequest);

        IProcessResponse CreateFile(ImageProcessRequest imageProcessRequest);

        IProcessResponse EditFile(ImageProcessRequest imageProcessRequest);
    }
}
