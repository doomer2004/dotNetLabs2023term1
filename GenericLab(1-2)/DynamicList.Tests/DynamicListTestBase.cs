namespace DynamicList.Tests;

public class DynamicListTestBase
{
    protected DynamicList<int> _dynamicList;

    protected DynamicListTestBase()
    {
        _dynamicList = new DynamicList<int>(new int[] {1, 2, 3, 4, 5});
    }
}