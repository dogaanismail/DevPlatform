using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Question
{
    public partial class QuestionComment : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        [ForeignKey("CommentQuestion")]
        public int QuestionId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(QuestionId), OtherKey = nameof(Question.Id), CanBeNull = true)]
        public virtual Question CommentQuestion { get; set; }
    }
}
