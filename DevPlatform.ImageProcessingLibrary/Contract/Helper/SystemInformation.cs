using DevPlatform.ImageProcessingLibrary.Contract.Enums;

namespace DevPlatform.ImageProcessingLibrary.Contract.Helper
{
    public class SystemInformation
    {
        public LibraryType LibraryType {get;set;}
        public PostType PostType { get; set; }
        public RequestFileType RequestFileType { get; set; } 
        public SystemType SystemType { get; set; }
    }
}
