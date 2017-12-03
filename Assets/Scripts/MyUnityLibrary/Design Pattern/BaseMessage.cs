public class BaseMessage
{
    public string Name;

    public BaseMessage()
    {
        Name = GetType().Name;
    }
}
