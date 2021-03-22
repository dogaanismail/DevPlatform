using DevPlatform.ImageProcessingLibrary.Contract.Helper;

namespace DevPlatform.ImageProcessingLibrary.Contract.Request
{
    public class ImageProcessRequest
    {
        public ImageProcessRequest()
        {
            FileInformation = new FileInformation();
            SystemInformation = new SystemInformation();
        }

        public FileInformation FileInformation { get; set; }  
        public SystemInformation SystemInformation { get; set; }
    }
}
