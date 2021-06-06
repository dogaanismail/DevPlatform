using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Question;
using QuestionClass = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Data.Mapping.Builders.Question
{
    public partial class QuestionCommentBuilder : DevPlatformEntityBuilder<QuestionComment>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(QuestionComment.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(QuestionComment.QuestionId)).AsInt32().NotNullable().ForeignKey<QuestionClass>(onDelete: Rule.Cascade)
               .WithColumn(nameof(QuestionComment.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(QuestionComment.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(QuestionComment.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(QuestionComment.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(QuestionComment.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
