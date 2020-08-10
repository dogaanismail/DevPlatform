﻿using System;
using System.Security.Claims;

namespace DevPlatform.LinqToDB.Identity
{
	/// <summary>
	///     Represents a claim that a user possesses.
	/// </summary>
	/// <typeparam name="TKey">The type used for the primary key for this user that possesses this claim.</typeparam>
	public interface IIdentityUserClaim<TKey> : IClameConverter
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key of the user associated with this claim.
		/// </summary>
		TKey UserId { get; set; }

		/// <summary>
		///     Gets or sets the claim type for this claim.
		/// </summary>
		string ClaimType { get; set; }

		/// <summary>
		///     Gets or sets the claim value for this claim.
		/// </summary>
		string ClaimValue { get; set; }
	}

	/// <summary>
	///     Represents a claim that a user possesses.
	/// </summary>
	/// <typeparam name="TKey">The type used for the primary key for this user that possesses this claim.</typeparam>
	public class IdentityUserClaim<TKey> :
#if NETSTANDARD2_0
	Microsoft.AspNetCore.Identity.IdentityUserClaim<TKey>,
#endif
		IIdentityUserClaim<TKey>, IClameConverter where TKey : IEquatable<TKey>
	{
#if !NETSTANDARD2_0
		/// <summary>
		///     Gets or sets the identifier for this user claim.
		/// </summary>
		public virtual int Id { get; set; }

		/// <summary>
		///     Gets or sets the primary key of the user associated with this claim.
		/// </summary>
		public virtual TKey UserId { get; set; }

		/// <summary>
		///     Gets or sets the claim type for this claim.
		/// </summary>
		public virtual string ClaimType { get; set; }

		/// <summary>
		///     Gets or sets the claim value for this claim.
		/// </summary>
		public virtual string ClaimValue { get; set; }

		/// <summary>
		///     Converts the entity into a Claim instance.
		/// </summary>
		/// <returns></returns>
		public virtual Claim ToClaim()
		{
			return new Claim(ClaimType, ClaimValue);
		}

		/// <summary>
		///     Reads the type and value from the Claim.
		/// </summary>
		/// <param name="claim"></param>
		public virtual void InitializeFromClaim(Claim claim)
		{
			ClaimType = claim.Type;
			ClaimValue = claim.Value;
		}
#endif
	}
}
