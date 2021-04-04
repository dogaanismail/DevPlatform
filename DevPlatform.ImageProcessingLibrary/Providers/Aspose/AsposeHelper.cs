using Aspose.Imaging.FileFormats.Apng;
using Aspose.Imaging.FileFormats.Bmp;
using Aspose.Imaging.FileFormats.Dicom;
using Aspose.Imaging.FileFormats.Gif;
using Aspose.Imaging.FileFormats.Jpeg;
using Aspose.Imaging.FileFormats.Jpeg2000;
using Aspose.Imaging.FileFormats.Png;
using Aspose.Imaging.ImageOptions;
using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using System;

namespace DevPlatform.ImageProcessingLibrary.Providers.Aspose
{
    /// <summary>
    /// Asponse helper implementations
    /// </summary>
    public class AsposeHelper
    {
        public static Tuple<Type, Type> GetFileTypes(RequestFileType requestFileType)
        {
            return requestFileType switch
            {
                RequestFileType.Bmp => new Tuple<Type, Type>(typeof(BmpOptions), typeof(BmpImage)),
                RequestFileType.Gif => new Tuple<Type, Type>(typeof(GifOptions), typeof(GifImage)),
                RequestFileType.Dicom => new Tuple<Type, Type>(typeof(DicomOptions), typeof(DicomImage)),
                RequestFileType.Jpeg => new Tuple<Type, Type>(typeof(JpegOptions), typeof(JpegImage)),
                RequestFileType.Jpeg2000 => new Tuple<Type, Type>(typeof(Jpeg2000Options), typeof(Jpeg2000Image)),
                RequestFileType.Png => new Tuple<Type, Type>(typeof(PngOptions), typeof(PngImage)),
                RequestFileType.Apng => new Tuple<Type, Type>(typeof(ApngOptions), typeof(ApngImage)),
                _ => new Tuple<Type, Type>(typeof(BmpOptions), typeof(BmpImage)),
            };
        }
    }

}
