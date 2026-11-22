using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowRuntime<T> : MonoBehaviour, IReadOnlyList<T>
{
    private readonly List<T> _items = new();

    private RowCollection<T> _collection;

    private RowCollection<T> Collection =>
        _collection ??= new RowCollection<T>(_items);

    public int Count => Collection.Count;

    public T this[int index] => Collection[index];

    public void Add(T item) => Collection.Add(item);

    public void Insert(int index, T item) =>
        Collection.Insert(index, item);

    public bool Remove(T item) => Collection.Remove(item);

    public void RemoveAt(int index) => Collection.RemoveAt(index);

    public void Clear() => Collection.Clear();

    public bool Contains(T item) => Collection.Contains(item);

    public int IndexOf(T item) => Collection.IndexOf(item);

    public T First() => Collection.First();

    public T Last() => Collection.Last();

    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

    System.Collections.IEnumerator
        System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}