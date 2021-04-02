using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;
using Aspose.Imaging.Sources;
using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;
using System;
using AsponseRectangle = Aspose.Imaging.Rectangle;
using AsposeImaging = Aspose.Imaging;

namespace DevPlatform.ImageProcessingLibrary.Providers.ImageSharp
{
    public class ImageSharpSaveFileRequest : IRequestHelper
    {
        public ImageProcessResponse CreateFileProcessRequest(ImageProcessRequest imageProcessRequest)
        {

            PngOptions options = new();


            using (PngImage image = (PngImage)Image.Create(options, width, height))
            {
                // Create and initialize an instance of Graphics class 
                // and Clear Graphics surface
                Graphics graphic = new Graphics(image);
                graphic.Clear(Color.Green);
                // Draw line on image
                graphic.DrawLine(new Pen(Color.Blue), 9, 9, 90, 90);

                // Resize image
                int newWidth = 400;
                image.ResizeWidthProportionally(newWidth, ResizeType.AdaptiveResample);

                // Crop the image to specified area
                AsponseRectangle area = new AsponseRectangle(10, 10, 200, 200);
                AsposeImaging.FileFormat.
                image.Crop(area);

                image.Save();
            }
        }
    }
}
