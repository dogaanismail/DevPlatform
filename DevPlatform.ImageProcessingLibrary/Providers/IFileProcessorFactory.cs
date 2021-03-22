using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using DevPlatform.ImageProcessingLibrary.Providers.CreateFile;
using DevPlatform.ImageProcessingLibrary.Providers.EditFile;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;
using DevPlatform.ImageProcessingLibrary.Providers.LoadFile;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public interface IFileProcessorFactory
    {
        IRequestHelper CreateRequest(PostType type);

        ILoadFile CreateLoad();

        IEditFile CreateEdit();

        ISaveFile CreateSave();
    }
}
