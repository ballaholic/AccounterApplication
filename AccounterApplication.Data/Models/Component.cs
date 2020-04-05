namespace AccounterApplication.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Component : BaseDeletableModel<string>, IUserEntity<string>, IActivatable
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        [Required]
        public int ComponentTypeId { get; set; }

        public ComponentType ComponentType { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
