
using DynamicList.CustomEventArgs;

namespace DynamicList.Tests;

public class DynamicListTest : DynamicListTestBase
{
    
    #region Constructor

    [Fact]
    public void Constructor_CreatingDynamicListWithoutCapacity_ReturnsTrue()
    {
        var collection = new DynamicList<int>();

        Assert.Empty(collection);
    }

    [Fact]
    public void Constructor_CreatingListWithNegativeCapacity_ThrowsException()
    {
        var capacity = -1;

        Assert.Throws<ArgumentOutOfRangeException>(() => new DynamicList<int>(capacity));
    }

    [Fact]
    public void Constructor_CreatingNull_List_ThrowsNullException()
    {
        IEnumerable<int> collection = null;

        Assert.Throws<ArgumentNullException>(
            () => collection = new DynamicList<int>(collection));
    }

    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    public void Constructor_CreatingDynamicListWithCapacity_ReturnsTrue(int capacity)
    {
        var collection = new DynamicList<int>(capacity);

        Assert.Empty(collection);
    }

    [Theory]
    [InlineData(new int[] {1, 2, 3, 4, 5})]
    [InlineData(new int[] {-1, -2, -3})]
    [InlineData(new int[] {0, 0, 0, 0, 0})]
    public void Constructor_CreatingDynamicListWithItems_ReturnsTrue(IEnumerable<int> items)
    {
        var collection = new DynamicList<int>(items);
        
        Assert.Equal(items, collection);
    }
    #endregion

    #region Add
    [Fact]
    public void Add_AddingItemToTheList_ReturnsTrue()
    {
        var item = 6;
        
        _dynamicList.Add(item);
        
        Assert.Contains(item, _dynamicList);
    }
    
    [Fact]
    public void Add_IncreasingCapacity_ReturnsTrue()
    {
        var item = 6;
        var expectedCapacity = 1;
        var initialCapacity = _dynamicList.Count;
        
        _dynamicList.Add(item);
        
        Assert.Equal(expectedCapacity, _dynamicList.Count - initialCapacity);
        
    }
    #endregion
    
    #region Insert
    [Fact]
    public void Insert_AddingItemToTheList_ReturnsTrue()
    {
        var item = 6;
        
        _dynamicList.Insert(5, item);
        
        Assert.Contains(item, _dynamicList);
    }

    [Fact]
    public void Insert_PassingInvalidIndex_ThrowsException()
    {
        var index = 6;
        var item = 6;

        Assert.Throws<InvalidOperationException>(
            () => _dynamicList.Insert(index, item));
    }
    
    #endregion
    
    #region Remove
    
    [Fact]
    public void Remove_RemovingItemFromTheList_ReturnsTrue()
    {
        var item = 5;
        
        _dynamicList.Remove(item);
        
        Assert.DoesNotContain(item, _dynamicList);
    }
    #endregion

    #region RemoveAt

    [Fact]
    public void RemoveAt_RemoveAtInvalidIndex_ThrowsException()
    {
        var index = 6;

        Assert.Throws<ArgumentOutOfRangeException>(
            () => _dynamicList.RemoveAt(index));

    }
    
    [Fact]
    public void RemoveAt_RemovingItemFromTheList_ReturnsTrue()
    {
        var index = 4;
        
        _dynamicList.RemoveAt(index);
        
        Assert.DoesNotContain(_dynamicList[index], _dynamicList);
    }

    #endregion


    #region Clear

    [Fact]
    public void Clear_ClearingTheList_ReturnsTrue()
    {
        _dynamicList.Clear();
        
        Assert.Empty(_dynamicList);
    }

    #endregion

    #region IndexOf

    [Fact]
    public void IndexOf_FindingItemInTheList_ReturnsTrue()
    {
        var item = 5;
        
        var index = _dynamicList.IndexOf(item);
        
        Assert.Equal(4, index);
    }


    #endregion
    
    
    #region Contains
    
    [Fact]
    public void Contains_FindingItemInTheList_ReturnsTrue()
    {
        var item = 5;
        
        var contains = _dynamicList.Contains(item);
        
        Assert.True(contains);
    }

    [Fact]
    public void Contains_NotFindingItemInTheList_ReturnsTrue()
    {
        var item = 0;
        
        Assert.DoesNotContain(item, _dynamicList);
    }
    #endregion

    #region CopyTo
    
    [Fact]
    public void CopyTo_CopyingTheList_ReturnsTrue()
    {
        var lst = new int[10];
        
        _dynamicList.CopyTo(lst, 0);
        
        Assert.Equal(new int[]{1, 2, 3, 4, 5, 0, 0, 0, 0, 0}, lst);
    }
    
    [Fact]
    public void CopyTo_PassingInvalidIndex_ThrowsException()
    {
        var lst = new int[10];
        var index = 6;

        Assert.Throws<ArgumentException>(() => _dynamicList.CopyTo(lst, index));
    }

    #endregion


    #region Count

    [Fact]
    public void Count_CountingTheList_ReturnsTrue()
    {
        var expectedCount = 5;
        
        var count = _dynamicList.Count;
        
        Assert.Equal(expectedCount, count);
    }
    
    #endregion

    #region Set

    [Theory]
    [InlineData (0, 1)]
    [InlineData (1, 2)]
    [InlineData (2, 3)]
    [InlineData (3, 4)]
    [InlineData (4, 5)]
    public void Set_SettingItemInTheList_ReturnsTrue(int index, int item)
    {
        _dynamicList[index] = item;
        
        Assert.Equal(item, _dynamicList[index]);
    }

    [Fact]
    public void Set_PassingInvalidIndex_ThrowsException()
    {
        var index = 6;
        var item = 6;
        
        Assert.Throws<ArgumentException>(()=> _dynamicList[index] = item);
    }

    #endregion
    
}