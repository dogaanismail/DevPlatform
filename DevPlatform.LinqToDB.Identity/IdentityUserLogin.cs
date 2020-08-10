﻿using System;

namespace DevPlatform.LinqToDB.Identity
{
    /// <summary>
    ///     Represents a login and its associated provider for a user.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key of the user associated with this login.</typeparam>
    public class IdentityUserLogin<TKey> :
#if NETSTANDARD2_0
	Microsoft.AspNetCore.Identity.IdentityUserLogin<TKey>,
#endif
		IIdentityUserLogin<TKey> where TKey : IEquatable<TKey>
	{
#if !NETSTANDARD2_0
		/// <summary>
		///     Gets or sets the login provider for the login (e.g. facebook, google)
		/// </summary>
		public virtual string LoginProvider { get; set; }

		/// <summary>
		///     Gets or sets the unique provider identifier for this login.
		/// </summary>
		public virtual string ProviderKey { get; set; }

		/// <summary>
		///     Gets or sets the friendly name used in a UI for this login.
		/// </summary>
		public virtual string ProviderDisplayName { get; set; }

		/// <summary>
		///     Gets or sets the of the primary key of the user associated with this login.
		/// </summary>
		public virtual TKey UserId { get; set; }
#endif
	}
}
