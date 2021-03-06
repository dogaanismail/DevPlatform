﻿using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppRoleClaim : BaseEntity, IAppRoleClaim
    {
        [Required, Identity]
        [Key]
        public new int Id { get; set; }

        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        /// <summary>
        /// Reads the type and value from the Claim.
        /// </summary>
        /// <param name="other"></param>
        public void InitializeFromClaim(Claim other)
        {
            ClaimType = other.Type;
            ClaimValue = other.Value;
        }

        /// <summary>
        /// Converts the entity into a Claim instance.
        /// </summary>
        /// <returns></returns>
        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }
    }
}
