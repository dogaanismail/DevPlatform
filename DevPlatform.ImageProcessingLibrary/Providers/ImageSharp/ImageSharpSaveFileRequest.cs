using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.ImageSharp
{
    public class ImageSharpSaveFileRequest : IRequestHelper
    {
        public ImageProcessResponse CreateFileProcessRequest(ImageProcessRequest imageProcessRequest)
        {
            return new ImageProcessResponse();
        }
    }
}
