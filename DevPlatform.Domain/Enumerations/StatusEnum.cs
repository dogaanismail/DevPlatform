using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Domain.Enumerations
{
    public enum StatusEnum
    {
        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Pssive")]
        Passive = 2,

        [Display(Name = "Deleted")]
        Deleted = 99,
    }
}
