using System.Collections;

namespace DynamicList.Enumerator;

public class DynamicListEnumerator<T> : IEnumerator<T> 
{
    private readonly IList<T> _list;
    private int _pointer;
    private T _current;
    public T Current => _current;
    object IEnumerator.Current => _current!;


    public DynamicListEnumerator(IList<T> list)
    {
        _list = list ?? throw new NullReferenceException(nameof(list));
        _pointer = -1;
        _current = _list.Any() ? _list[0] : default;
    }

    public bool MoveNext()
    {
        _pointer++;
        if (_pointer >= _list.Count) return false;
        _current = _list[_pointer];
        return true;
    }

    public void Reset()
    {
        _pointer = -1;
        if (_list.Any())
        {
            _current = _list[0];
        }
    }
    
    public void Dispose()
    {
    }
    
}