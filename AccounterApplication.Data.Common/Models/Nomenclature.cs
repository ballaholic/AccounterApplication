namespace AccounterApplication.Data.Common.Models
{
    public abstract class Nomenclature<TKey> : BaseDeletableModel<TKey>, ILocalizable
    {
        public string NameEN { get; set; }
        public string NameBG { get; set; }

        public bool IsMain { get; set; }
    }
}
