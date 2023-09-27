using System.Collections;
using System.Numerics;
using DynamicList.CustomEventArgs;
using GenericLab1;

namespace DynamicList;

public sealed class DynamicList<T> : IList<T> where T : struct
{
    private const int DefaultCapacity = 4;
    private int _size;
    private int _capacity;
    private T[] _items;
    public int Count => _size;
    public bool IsReadOnly { get; } = false;

    public DynamicList()
    {
        _capacity = DefaultCapacity;
        _items = new T[_capacity];
    }
    public DynamicList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        else if(capacity > 0)
        {
            _capacity = DefaultCapacity;
            _items = new T[_capacity];
        }
        else
        {
            _capacity = capacity;
            _size = 0;
            _items = Array.Empty<T>();
        }
    }

    public DynamicList(IEnumerable<T> items)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));
        _capacity = DefaultCapacity;
        _items = new T[_capacity];
        foreach (var item in items)
        {
            this.Add(item);
        }
    }
    
    public EventHandler<ArrayItemEventArgs<T>> ItemAdded;

    public EventHandler<ArrayItemEventArgs<T>> ItemRemoved;

    public EventHandler<ArrayEventArgs> DynamicListCleared;

    public EventHandler<ArrayResizedEventArgs> DynamicListResized;

    private void OnItemAdded(T item, int index)
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, new ArrayItemEventArgs<T>(item, index, ArrayAction.Add));
        }
    }

    private void OnItemRemoved(T item, int index)
    {
        if (ItemRemoved != null)
        {
            ItemRemoved(this, new ArrayItemEventArgs<T>(item, index, ArrayAction.Remove));
        }
    }

    private void OnArrayCleared()
    {
        if (DynamicListCleared != null)
        {
            DynamicListCleared(this, new ArrayEventArgs(ArrayAction.Clear));
        }
    }

    private void OnArrayResized(int oldCapacity)
    {
        if (DynamicListResized != null)
        {
            DynamicListResized(this, new ArrayResizedEventArgs(oldCapacity, _capacity));
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new DynamicListEnumerator<T>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        if (_size >= _capacity)
        {
            Resize();
        }
    
        _items[_size] = item;
        _size++;
    }
    
    public void Clear()
    {
        _items = new T[DefaultCapacity];
        _capacity = _size;
        
        OnArrayCleared();
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            var elements = _items[i];

            if (elements.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array.Length - arrayIndex < _items.Length)
            throw new ArgumentException("Dest array is too small");
        
        Array.Copy(_items, 0, 
            array, arrayIndex, _items.Length );
    }

    public bool Remove(T item)
    {
        var index = Array.IndexOf(_items, item);
        var isRemoved = index != -1;
        RemoveAt(index);
        return isRemoved;
    }

    private void Resize()
    {
        var oldCapacity = _capacity;
        var newCapacity = _capacity * 2;
        var tempArray = new T[newCapacity];
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
        
        OnArrayResized(oldCapacity);
    }

    public int IndexOf(T item)
    {
        return Array.IndexOf(_items, item);
    }

    public void Insert(int index, T item)
    {
        if (_size < index)
            throw new InvalidOperationException("Invalid index");
        if (_size == index)
            _items[index] = item;
        if (_size == _capacity)
            Resize();
        int properIndex = GetProperIndex(index, toInsert: true);
        _items[properIndex] = item;
        _size++;
        OnItemAdded(item, index);
    }
    private int GetProperIndex(int index, bool toInsert = false)
    {
        if (_size == 0 && !toInsert)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        int count = toInsert ? _size + 1 : _size;

        return index >= 0 ? index % count : (count + (index % count)) % count;
    }
    public void RemoveAt(int index)
    {
        if (index < 0 || index > _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        var item = _items[index];
        _size--;
        Array.Copy(_items, index + 1,
            _items, index, _size - index);
        OnItemRemoved(item, index);
    }

    public T this[int index]
    {
        get => _items[index];
        set
        {
            if (index >= _size)
            {
                throw new ArgumentException("Invalid index");
            }
            
            _items[index] = value;
        }
    }
    
}
