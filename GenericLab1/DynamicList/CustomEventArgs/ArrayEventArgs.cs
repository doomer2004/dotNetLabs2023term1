namespace DynamicList.CustomEventArgs;

public class ArrayEventArgs : EventArgs
{
    public ArrayAction Action { get; private set; }
    public TimeOnly ActionTime { get; private set; }
        
    public ArrayEventArgs(ArrayAction action)
    {
        Action = action;
        ActionTime = TimeOnly.FromDateTime(DateTime.Now);
            ;
    }
}