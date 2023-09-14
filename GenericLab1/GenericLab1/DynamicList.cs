using System.Collections;
using System.Numerics;

namespace GenericLab1;

public class DynamicList<T> : ICollection<T> where T : struct, IComparable<T>
{
    private Vector<T>[] _items;
    public int Count { get; private set; }
    public bool IsReadOnly { get; } = false;
    public int Version { get; }

    public DynamicList()
    {
        _items = new Vector<T>[4];
        Count = 0;
    }
    public IEnumerator<T> GetEnumerator()
    {
        IEnumerator enumerator = new DynamicListEnumerator<T>(_items);
        return (IEnumerator<T>) enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        var tempItem = new Vector<T>(item);
        if (Count == _items.Length)
            Array.Resize(ref _items, _items.Length*2);
        _items[Count] = tempItem;
        Count++;
    }
    
    public void Clear()
    {
        Array.Clear(_items, 0, Count);
        Count = 0;
    }

    public bool Contains(T item)
    {
        var found = false;
        foreach (var vector in _items)
        {
            _items.Contains<>(item);
        }
        return found;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        for (var i = 0; i < _items.Length; i++)
        {
            _items[i] = new Vector<T>(array[i]);
        }
    }

    public bool Remove(T item)
    {
        var deleted = false;
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items.Contains<>(item))
            {
                _items[i] = new Vector<T>();
                deleted = true;
            }
        }

        return deleted;
    }


}