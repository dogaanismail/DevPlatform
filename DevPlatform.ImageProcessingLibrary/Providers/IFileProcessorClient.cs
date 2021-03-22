using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public interface IFileProcessorClient
    {
        void Load(ImageProcessRequest imageProcessRequest);

        SaveFileResponse ApplySave();
        EditFileResponse ApplyEdit();
        LoadFileResponse ApplyLoad();
    }
}
