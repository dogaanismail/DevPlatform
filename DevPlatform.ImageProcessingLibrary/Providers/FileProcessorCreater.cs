using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using DevPlatform.ImageProcessingLibrary.Providers.Aspose;
using DevPlatform.ImageProcessingLibrary.Providers.ImageSharp;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    /// <summary>
    /// FileProcessorCreater class implementations
    /// </summary>
    public class FileProcessorCreater : IFileProcessorCreater
    {
        public IFileProcessorFactory ProcessorFactory(LibraryType libraryType)
        {
            switch (libraryType)
            {
                case LibraryType.Aspose:
                    return new AsposeFactory();
                case LibraryType.ImageSharp:
                    return new ImageSharpFactory();
                default:
                    return null;
            }
        }
    }
}
