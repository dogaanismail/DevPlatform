using DevPlatform.ImageProcessingLibrary.Contract.Enums;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public interface IFileProcessorCreater
    {
        IFileProcessorFactory ProcessorFactory(LibraryType libraryType);
    }
}
