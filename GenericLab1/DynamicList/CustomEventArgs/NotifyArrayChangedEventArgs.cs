namespace DynamicList.CustomEventArgs;

public class NotifyArrayChangedEventArgs<T>
{
    public ArrayAction Action { get; private set; }
    public int ActionTime { get; private set; }
    
    public int OldCapacity { get; private set; }
    public int NewCapacity { get; private set; }
    
    public T Item { get; private set; }
    public int Index { get; private set; }
    
    public NotifyArrayChangedEventArgs(ArrayAction action)
    {
        Action = action;
    }
    
    public NotifyArrayChangedEventArgs( ArrayAction action, int oldCapacity, int newCapacity) : this(action)
    {
        Action = action;
        OldCapacity = oldCapacity;
        NewCapacity = newCapacity;
        ActionTime = DateTime.Now.Millisecond;
    }
    
    public NotifyArrayChangedEventArgs(ArrayAction action, T item, int index) : this(action)
    {
        Action = action;
        Item = item;
        Index = index;
        ActionTime = DateTime.Now.Millisecond;
    }
    
}