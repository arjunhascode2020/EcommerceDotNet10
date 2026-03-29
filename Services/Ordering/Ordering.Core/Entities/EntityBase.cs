namespace Ordering.Core.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? LastModifyBy { get; set; }
        public DateTime? LastUpdateAt { get; set; }
    }
}
