using System.Collections;
using System.Numerics;
using GenericLab1;

namespace DynamicList;

public class DynamicList<T> : IList<T>, IComparable<T> where T : struct
{
    private const int DefaultCapacity = 4;
    private int _size;
    private int _capacity;
    private Vector<T>[] _items;
    public int Count => _size;
    public bool IsReadOnly { get; } = false;
    
    public event Action? ItemRemoved;
    public event Action? CollectionCleared;
    public event Action? CollectionResized;
    
    public DynamicList(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }
        else if(capacity > 0)
        {
            _capacity = DefaultCapacity;
            _items = new Vector<T>[_capacity];
        }
        else
        {
            _capacity = capacity;
            _size = 0;
            _items = new[] {Vector<T>.Zero} ;

        ;
        }
    }

    public DynamicList(IEnumerable<T> items)
    {
        if (items is null)
            throw new ArgumentNullException(nameof(items));
        _capacity = DefaultCapacity;
        _items = new Vector<T>[_capacity];
        foreach (var item in items)
        {
            this.Add(item);
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
        
        _items[_size] = new Vector<T>(item);
        _size++;

    }
    
    public void Clear()
    {
        _items = new Vector<T>[DefaultCapacity];
        _capacity = _size;
        //CollectionCleared.Invoke();
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < _size; i++)
        {
            var elements = _items[i];

            if (elements.Equals(item) == true)
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
        ItemRemoved.Invoke();
    }

    private void Resize()
    {
        var newCapacity = _capacity * 2;
        var tempArray = new Vector<T>[newCapacity];
        Array.Copy(_items, tempArray, _size);
        _items = tempArray;
        _capacity = newCapacity;
        //CollectionResized.Invoke();
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
            _items[index] = new Vector<T>(item);
        if (_size == _capacity)
            Resize();
        _size++;
        Array.Copy(_items, 0, 
            _items, index + 1, _items.Length);
        _items[index] = new Vector<T>(item);
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
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int vectorIndex = index / Vector<T>.Count;
            int elementIndex = index % Vector<T>.Count;

            return _items[vectorIndex][elementIndex];
        }

        set => throw new NotImplementedException();
    }


    public int CompareTo(T other)
    {
        return 0;
    }
}
