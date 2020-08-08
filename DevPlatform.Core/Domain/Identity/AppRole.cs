using DevPlatform.Core.Entities;
using LinqToDB.Identity;
using System;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppRole : IdentityRole<int>, IEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? StatusId { get; set; }
    }
}
