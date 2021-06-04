using DevPlatform.Core.Entities;
using LinqToDBAssociation = LinqToDB.Mapping;
using System.Collections.Generic;

namespace DevPlatform.Core.Domain.Question
{
    public partial class Question : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(QuestionComment.QuestionId), CanBeNull = true)]
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
    }
}
