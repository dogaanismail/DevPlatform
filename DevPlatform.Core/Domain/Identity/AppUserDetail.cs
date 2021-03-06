﻿using DevPlatform.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserDetail : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(100)]
        public string ProfilePhotoPath { get; set; }

        [MaxLength(100)]
        public string CoverPhotoPath { get; set; }

        [MaxLength(39)]
        public string City { get; set; }

        [MaxLength(35)]
        public string Country { get; set; }

        [MaxLength(250)]
        public string AboutMe { get; set; }

        [MaxLength(10)]
        public string Sex { get; set; }

        [MaxLength(50)]
        public string UniversityName { get; set; }

        public DateTime? UniStartDate { get; set; }

        public DateTime? UniFinishUpDate { get; set; }

        public bool? HasGraduated { get; set; }

        [MaxLength(200)]
        public string UniversityDesc { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(50)]
        public string Designation { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(AppUser.DetailId), CanBeNull = false)]
        public virtual AppUser UserDetail { get; set; }
    }
}
