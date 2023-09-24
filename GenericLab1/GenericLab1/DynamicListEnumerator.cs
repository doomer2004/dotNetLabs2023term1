﻿using System.Collections;
using System.Numerics;

namespace GenericLab1;

public class DynamicListEnumerator<T> : IEnumerator<T> 
{
    private readonly IList<T> _list;
    private int _pointer;
    private T _current;
    public T Current => _current;
    object IEnumerator.Current => _current!;


    public DynamicListEnumerator(IList<T> list)
    {
        _list = list;
        _pointer = -1;
        _current = _list.Any() ? _list[_pointer] : default;
    }

    public bool MoveNext()
    {
        if (!HasNext())
        {
            return false;
        }
        _current = _list[++_pointer];
        return HasNext();
    }

    public void Reset()
    {
        _pointer = -1;
        _current = _list[0];
    }
    
    public void Dispose()
    {
    }

    private bool HasNext()
    {
        return _pointer < _list.Count - 1;
    }
}