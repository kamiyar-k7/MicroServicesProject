
namespace EventBusMessages.Events;

public class IntegrationBaseEvent
{

    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreateData = DateTime.UtcNow;
    }

    public IntegrationBaseEvent(Guid id , DateTime createTime)
    {
        
        Id = id;
        CreateData = createTime;

    }

    public Guid Id { get; set; }

    public DateTime CreateData { get; set; }

}
