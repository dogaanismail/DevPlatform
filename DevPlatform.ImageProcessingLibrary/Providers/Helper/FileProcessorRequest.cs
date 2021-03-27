using DevPlatform.ImageProcessingLibrary.Contract.Request;

namespace DevPlatform.ImageProcessingLibrary.Providers.Helper
{
    /// <summary>
    /// FileProcesor Request class implementations
    /// </summary>
    public class FileProcessorRequest
    {
        public IRequestHelper Helper { get; set; }
        public ImageProcessRequest RequestInformation { get; set; }
    }
}
