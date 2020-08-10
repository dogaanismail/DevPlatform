﻿using System;

namespace DevPlatform.LinqToDB.Identity
{
    /// <summary>
    ///     Represents an authentication token for a user.
    /// </summary>
    /// <typeparam name="TKey">The type of the primary key used for users.</typeparam>
    public interface IIdentityUserToken<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key of the user that the token belongs to.
		/// </summary>
		TKey UserId { get; set; }

		/// <summary>
		///     Gets or sets the LoginProvider this token is from.
		/// </summary>
		string LoginProvider { get; set; }

		/// <summary>
		///     Gets or sets the name of the token.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		///     Gets or sets the token value.
		/// </summary>
		string Value { get; set; }
	}

	/// <summary>
	///     Represents an authentication token for a user.
	/// </summary>
	/// <typeparam name="TKey">The type of the primary key used for users.</typeparam>
	public class IdentityUserToken<TKey> :
#if NETSTANDARD2_0
	Microsoft.AspNetCore.Identity.IdentityUserToken<TKey>,
#endif
		IIdentityUserToken<TKey> where TKey : IEquatable<TKey>
	{
#if !NETSTANDARD2_0
		/// <summary>
		///     Gets or sets the primary key of the user that the token belongs to.
		/// </summary>
		public virtual TKey UserId { get; set; }

		/// <summary>
		///     Gets or sets the LoginProvider this token is from.
		/// </summary>
		public virtual string LoginProvider { get; set; }

		/// <summary>
		///     Gets or sets the name of the token.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		///     Gets or sets the token value.
		/// </summary>
		public virtual string Value { get; set; }
#endif
	}
}
