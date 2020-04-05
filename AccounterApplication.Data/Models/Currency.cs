namespace AccounterApplication.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Currency : Nomenclature<int>
    {
        public string Sign { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
