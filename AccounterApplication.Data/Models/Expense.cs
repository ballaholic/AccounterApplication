namespace AccounterApplication.Data.Models
{
    using Data.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static AccounterApplication.Common.GlobalConstants.ExpenseConstants;

    public class Expense : BaseDeletableModel<int>, IUserEntity<string>
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExpenseAmount { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int ExpenseGroupId { get; set; }
        
        public ExpenseGroup ExpenseGroup { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
