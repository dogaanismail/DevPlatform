using Aspose.Imaging;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Contract.Response.Aspose
{
    public class AsposeResponse : ImageProcessResponse
    {
        public RasterCachedImage ResultImage { get; set; }
    }
}
