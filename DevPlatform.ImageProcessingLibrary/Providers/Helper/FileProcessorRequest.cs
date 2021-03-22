using DevPlatform.ImageProcessingLibrary.Contract.Request;

namespace DevPlatform.ImageProcessingLibrary.Providers.Helper
{
    public class FileProcessorRequest
    {
        public IRequestHelper Helper { get; set; }
        public ImageProcessRequest RequestInformation { get; set; }
    }
}
