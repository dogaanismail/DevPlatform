using DevPlatform.Domain.Api;
using DevPlatform.Domain.Api.StoryApi;

namespace DevPlatform.Common.Helpers
{
    /// <summary>
    /// CheckItemType extension method
    /// </summary>
    public static class CheckItemType
    {
        #region Methods

        public static bool HasItemImage(PostCreateApi model)
        {
            if (model.Images != null && model.Images.Count > 0) return true;
            return false;
        }

        public static bool HasItemImage(StoryCreateApi model)
        {
            if (model.Photo != null && model.Photo.Length > 0) return true;
            return false;
        }

        public static bool HasItemVideo(PostCreateApi model)
        {
            if (model.Video != null && model.Video.Length > 0) return true;
            return false;
        }

        public static bool HasItemVideo(StoryCreateApi model)
        {
            if (model.Video != null && model.Video.Length > 0) return true;
            return false;
        }

        #endregion
    }
}
