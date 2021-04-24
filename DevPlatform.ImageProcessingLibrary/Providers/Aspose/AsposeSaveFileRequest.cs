using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Png;
using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;
using AsponseRectangle = Aspose.Imaging.Rectangle;
using Aspose.Imaging.ImageOptions;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Aspose;

namespace DevPlatform.ImageProcessingLibrary.Providers.Aspose
{
    /// <summary>
    /// Asponse save file request implementations
    /// </summary>
    public class AsposeSaveFileRequest : IRequestHelper
    {
        #region Methods

        /// <summary>
        /// Asponse create file request
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        public ImageProcessResponse CreateFileProcessRequest(ImageProcessRequest imageProcessRequest)
        {
            var fileInformation = imageProcessRequest.FileInformation;
            var systemInformation = imageProcessRequest.SystemInformation;

            PngOptions options = new();

            using (PngImage image = (PngImage)Image.Create(options, fileInformation.Width, fileInformation.Height))
            {
                Graphics graphic = new(image);
                graphic.Clear(Color.Green);
                graphic.DrawLine(new Pen(Color.Blue), 9, 9, 90, 90);

                // Resize image
                int newWidth = 400;
                image.ResizeWidthProportionally(newWidth, ResizeType.AdaptiveResample);

                // Crop the image to specified area
                AsponseRectangle area = new AsponseRectangle(10, 10, 200, 200);

                image.Crop(area);

                image.Save();

                return new AsposeResponse { Succeeded = true, ResultImage = image };
            }
        }

        #endregion
    }
}
