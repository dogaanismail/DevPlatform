using DevPlatform.LinqToDB.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevPlatform.Core.Domain.Identity
{
    public interface IAppUser : IIdentityUser<int>
    {
    }
}
