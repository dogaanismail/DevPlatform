﻿using System;

namespace DevPlatform.LinqToDB.Identity
{
    /// <summary>
    ///     Represents the link between a user and a role.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key used for users and roles.</typeparam>
    public class IdentityUserRole<TKey> :
#if NETSTANDARD2_0
	Microsoft.AspNetCore.Identity.IdentityUserRole<TKey>,
#endif
		IIdentityUserRole<TKey> where TKey : IEquatable<TKey>
	{
#if !NETSTANDARD2_0
		/// <summary>
		///     Gets or sets the primary key of the user that is linked to a role.
		/// </summary>
		public virtual TKey UserId { get; set; }

		/// <summary>
		///     Gets or sets the primary key of the role that is linked to the user.
		/// </summary>
		public virtual TKey RoleId { get; set; }
#endif
	}
}
