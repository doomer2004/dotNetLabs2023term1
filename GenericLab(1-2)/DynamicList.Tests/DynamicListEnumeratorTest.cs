using DynamicList.CustomEventArgs;
namespace DynamicList.Tests;


public class DynamicListEnumeratorTest : DynamicListTestBase
{
    
    [Fact]
    public void GetEnumerator_GetLastElement_ReturnsFalse()
    {
        var collection = new DynamicList<int>(new int[] {1, 2});
        var enumerator = collection.GetEnumerator();

        enumerator.MoveNext();
        enumerator.MoveNext();
        
        Assert.False(enumerator.MoveNext());
    }

    [Fact]
    public void Enumerator_Reset_ReturnsTrue()
    {
        var enumerator = _dynamicList.GetEnumerator();
        var expectedItem = 1;

        enumerator.MoveNext();
        enumerator.MoveNext();
        enumerator.Reset();
        
        Assert.Equal(expectedItem, enumerator.Current);
    }

}