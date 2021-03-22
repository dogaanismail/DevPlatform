using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using System;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    public class FileProcessorCreater : IFileProcessorCreater
    {
        public IFileProcessorFactory ProcessorFactory(LibraryType libraryType)
        {
            switch (libraryType)
            {
                case LibraryType.Aspose:
                    return null;
                case LibraryType.ImageSharp:
                    return null;
                default:
                    return null;
            }
        }
    }
}
