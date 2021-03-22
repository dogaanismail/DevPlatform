using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Providers.Helper
{
    public interface IRequestHelper
    {
        SaveFileResponse CreateSaveFileRequest(ImageProcessRequest imageProcessRequest);
        EditFileResponse EditSaveFileRequest(ImageProcessRequest imageProcessRequest);
        LoadFileResponse LoadSaveFileRequest(ImageProcessRequest imageProcessRequest);
    }
}
