using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowCollection<T> : IReadOnlyList<T>
{
    private readonly List<T> _items;

    public RowCollection(List<T> items)
    {
        _items = items;
    }

    public int Count => _items.Count;

    public T this[int index] => _items[index];

    public void Add(T item) => _items.Add(item);

    public void Insert(int index, T item) => _items.Insert(index, item);

    public bool Remove(T item) => _items.Remove(item);

    public void RemoveAt(int index) => _items.RemoveAt(index);

    public void Clear() => _items.Clear();

    public bool Contains(T item) => _items.Contains(item);

    public int IndexOf(T item) => _items.IndexOf(item);

    public T First()
    {
        if (Count == 0)
            throw new InvalidOperationException("Row is empty.");

        return _items[0];
    }

    public T Last()
    {
        if (Count == 0)
            throw new InvalidOperationException("Row is empty.");

        return _items[Count - 1];
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}