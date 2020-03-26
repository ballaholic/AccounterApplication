namespace AccounterApplication.Data.Common.Models
{
    public interface IUserEntity<TKey>
    {
        TKey UserId { get; set; }
    }
}
