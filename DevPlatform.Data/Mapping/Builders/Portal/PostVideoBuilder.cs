using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostVideoBuilder : DevPlatformEntityBuilder<PostVideo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
             .WithColumn(nameof(PostVideo.VideoUrl)).AsString(300).NotNullable()
             .WithColumn(nameof(PostVideo.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade)
             .WithColumn(nameof(PostVideo.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
             .WithColumn(nameof(PostVideo.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
             .WithColumn(nameof(PostVideo.CreatedDate)).AsDateTime().NotNullable()
             .WithColumn(nameof(PostVideo.ModifiedDate)).AsDateTime().Nullable()
             .WithColumn(nameof(PostVideo.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
