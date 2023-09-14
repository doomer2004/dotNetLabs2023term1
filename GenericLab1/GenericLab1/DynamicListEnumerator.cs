using System.Collections;
using System.Numerics;

namespace GenericLab1;

public class DynamicListEnumerator<T> : IEnumerator<T> where T : struct
{
    private readonly Vector<T>[] _values;
    private int _position = -1;
    public T Current { get; }
    object IEnumerator.Current => Current;


    public DynamicListEnumerator(Vector<T>[] values)
    {
        _values = values;
    }

    public bool MoveNext()
    {
        _position++;
        return (_position < _values.Length);
    }

    public void Reset()
    {
        _position = -1;
    }

    public void Dispose()
    {
    }
}