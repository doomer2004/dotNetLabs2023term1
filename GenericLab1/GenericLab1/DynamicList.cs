using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace GenericLab1;

public class DynamicList<T> : IList<T>
{
    private const int DefaultCapacity = 4;
    private int _size;
    private int _capacity;
    private T[] _items;
    public int Count => _size;
    public bool IsReadOnly { get; } = false;
    
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

    /*public override string ToString()
    {
        var str = new StringBuilder();
        for (int i = 0; i < Count; i++)
        {
            str.Append(_items[i]);
        }

        return str.ToString();
    }*/

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
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            var elements = _items[i];

            if (elements?.Equals(item) == true)
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
        var newCapacity = _capacity * 2;
        var tempArray = new T[newCapacity];
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
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
        _size++;
        Array.Copy(_items, 0, 
            _items, index + 1, _items.Length);
        _items[index] = item;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > _size)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        _size--;
        Array.Copy(_items, index + 1,
            _items, index, _size - index);
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
