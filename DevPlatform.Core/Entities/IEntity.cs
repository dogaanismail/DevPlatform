using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Core.Entities
{
    public interface IEntity
    {
        [Required, Identity]
        [Key]
        int Id { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        DateTime CreatedDate { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        DateTime? ModifiedDate { get; set; }

        int? CreatedBy { get; set; }
        int? ModifiedBy { get; set; }

        int? StatusId { get; set; }
    }
}
