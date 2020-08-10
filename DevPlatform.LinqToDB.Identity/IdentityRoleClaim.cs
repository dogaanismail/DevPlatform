﻿using System;
using System.Security.Claims;

namespace DevPlatform.LinqToDB.Identity
{
	/// <summary>
	///     Represents a claim that is granted to all users within a role.
	/// </summary>
	/// <typeparam name="TKey">The type of the primary key of the role associated with this claim.</typeparam>
	public class IdentityRoleClaim<TKey> :
#if NETSTANDARD2_0
	Microsoft.AspNetCore.Identity.IdentityRoleClaim<TKey>,
#endif
		IClameConverter, IIdentityRoleClaim<TKey> where TKey : IEquatable<TKey>
	{
#if !NETSTANDARD2_0
		/// <summary>
		///     Constructs a new claim with the type and value.
		/// </summary>
		/// <returns></returns>
		public virtual Claim ToClaim()
		{
			return new Claim(ClaimType, ClaimValue);
		}

		/// <summary>
		///     Initializes by copying ClaimType and ClaimValue from the other claim.
		/// </summary>
		/// <param name="other">The claim to initialize from.</param>
		public virtual void InitializeFromClaim(Claim other)
		{
			ClaimType = other?.Type;
			ClaimValue = other?.Value;
		}

		/// <summary>
		///     Gets or sets the identifier for this role claim.
		/// </summary>
		public virtual int Id { get; set; }

		/// <summary>
		///     Gets or sets the of the primary key of the role associated with this claim.
		/// </summary>
		public virtual TKey RoleId { get; set; }

		/// <summary>
		///     Gets or sets the claim type for this claim.
		/// </summary>
		public virtual string ClaimType { get; set; }

		/// <summary>
		///     Gets or sets the claim value for this claim.
		/// </summary>
		public virtual string ClaimValue { get; set; }
#endif
	}
}
