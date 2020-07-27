namespace Automat.Infrastructure.Db.Models
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}
