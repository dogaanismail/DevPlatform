using FluentMigrator.Builders.Create.Table;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Data.Extensions;
using System.Data;
using QuestionClass = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Data.Mapping.Builders.Question
{
    public partial class QuestionBuilder : DevPlatformEntityBuilder<QuestionClass>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(QuestionClass.Title)).AsString(256).NotNullable()
               .WithColumn(nameof(QuestionClass.Description)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(QuestionClass.CreatedBy)).AsInt32().NotNullable().WithDefaultValue(0).ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(QuestionClass.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(QuestionClass.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(QuestionClass.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(QuestionClass.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
