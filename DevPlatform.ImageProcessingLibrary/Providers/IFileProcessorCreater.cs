using DevPlatform.ImageProcessingLibrary.Contract.Enums;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    /// <summary>
    /// IFileProcessorCreater interface implementations
    /// </summary>
    public interface IFileProcessorCreater
    {
        /// <summary>
        /// Builds the factory
        /// </summary>
        /// <param name="libraryType"></param>
        /// <returns></returns>
        IFileProcessorFactory ProcessorFactory(LibraryType libraryType);
    }
}
