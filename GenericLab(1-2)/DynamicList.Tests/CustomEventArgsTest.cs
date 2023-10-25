using DynamicList.CustomEventArgs;

namespace DynamicList.Tests;

public class CustomEventArgsTest : DynamicListTestBase
{

    [Fact]
    public void ArrayAction_AddToArray_ReturnsTrue()
    {
        var newItem = 6;
        TestArrayAction(ArrayAction.Add, list => _dynamicList.Insert(5 ,newItem),
            eventArgs => Assert.Equal(eventArgs.Item, newItem));
    }
    
    [Fact]
    public void ArrayAction_RemoveFromArray_ReturnsTrue()
    {
        var item = 5;
        TestArrayAction(ArrayAction.Remove, list => _dynamicList.Remove(item),
            eventArgs => Assert.Equal(eventArgs.Item, item));
    }
    
    [Fact]
    public void ArrayAction_ClearArray_ReturnsTrue()
    {
        TestArrayAction(ArrayAction.Clear, list => _dynamicList.Clear(),
            eventArgs => Assert.Empty(_dynamicList));
    }
    
    private void TestArrayAction(ArrayAction action, Action<DynamicList<int>> arrayAction,
        Action<NotifyArrayChangedEventArgs<int>> arrayChanged)
    {
        NotifyArrayChangedEventArgs<int>? eventArgs = null;
        _dynamicList.ArrayChanged+= args => eventArgs = args;
        
        arrayAction(_dynamicList);
        
        Assert.NotNull(eventArgs);
        Assert.Equal(eventArgs.Action, action);
        arrayChanged(eventArgs!);
    }
}