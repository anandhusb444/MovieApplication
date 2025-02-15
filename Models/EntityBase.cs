namespace MovieApplication.Models
{
    public class EntityBase
    {
        public Guid Id { get; private init; } = Guid.NewGuid();
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastModifed { get; private set; } = DateTimeOffset.UtcNow;

        public void UdateLastModified()
        {
            LastModifed = DateTimeOffset.UtcNow;
        }

    }
}
