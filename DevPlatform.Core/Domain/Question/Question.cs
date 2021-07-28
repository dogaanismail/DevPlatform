using DevPlatform.Core.Entities;
using LinqToDBAssociation = LinqToDB.Mapping;
using System.Collections.Generic;
using DevPlatform.Core.Domain.Common;

namespace DevPlatform.Core.Domain.Question
{
    public partial class Question : BaseEntity, ISoftDeletedEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(QuestionComment.QuestionId), CanBeNull = true)]
        public virtual ICollection<QuestionComment> QuestionComments { get; set; }
    }
}
