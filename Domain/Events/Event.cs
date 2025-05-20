namespace DDDExample.Domain.Events;

public class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = default!;
    public string Data { get; set; } = default!;
    public DateTime OccurredOn { get; set; }
}
