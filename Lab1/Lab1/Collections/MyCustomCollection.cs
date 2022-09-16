using System.Collections;
using System.Collections.Specialized;
using Lab1.Exceptions;

namespace Lab1.Collections;

using Interfaces;

public class MyCustomCollection<T> : IEnumerable<T>, ICustomCollection<T> where T : IComparable
{
    private class Node
    {
        public T Val;
        public Node? Next = null;
        public Node? Prev = null;


        public Node(T val, Node? next, Node? prev)
        {
            this.Val = val;
            this.Next = next;
            this.Prev = prev;
        }
    }

    



    private Node? _head = null;
    private Node? _tail = null;
    private Node? _cursor = null;
    private int _count = 0;

    public T this[int index]
    {
        get
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException("out of range");
            }
            
            var temp = _head;
            
            while (index != 0)
            {
                --index;
                temp = temp!.Next;
            }

            return temp!.Val;
        }
        set
        {
            
            if (index >= Count)
            {
                throw new IndexOutOfRangeException("out of range");
            }
            
            var temp = _head;
            
            while (_count != 0)
            {
                --_count;
                _head = _head!.Next;
            }

            _head!.Val = value;

        }
    }
    
    public void Reset()
    {
        _cursor = _head;
    }

    public void Next()
    {
        if (_cursor is null) throw new IndexOutOfRangeException("ero elements in the collection.");
        
        _cursor = _cursor.Next ?? throw new IndexOutOfRangeException("Cursor on the last element of the collection.");
    }

    public T Current()
    {
        if (_cursor is null) throw new InvalidOperationException("Zero elements in the collection.");
        
        return _cursor.Val;
    }

    public int Count
    {
        get 
        {
            return _count;
        }
    }

    public void Add(T item)
    {
        if (_tail == null) // если в коллекции ноль элементов
        {
            _head = new Node (item, null, null);
            _tail = _head;
            _cursor = _head;
            ++_count;
            return;
        }
        
        // проверяем нет ли в коллекции такого же элемента
        var temp = _head;
        while (temp != null)
        {
            if (temp.Val.CompareTo(item) == 0)
            {
                throw new InvalidOperationException("This item is already in the collection.");
            }

            temp = temp.Next;
        }
        
        // добавляем в конец
        _tail.Next = new Node(item, null, _tail);
        _tail = _tail.Next;

        ++_count;
    }

    public void Remove(T item)
    {

        var temp = _head;
        while (temp != null)
        {
            if (temp.Val.CompareTo(item) == 0)
            {
                if (temp.Prev == null && temp.Next == null)
                {
                    _head = null;
                    _tail = null;
                    
                    Reset();
                }
                else if (temp.Prev == null)
                {
                    _head = temp.Next;
                    temp!.Next!.Prev = temp.Prev;
                    
                    if (temp.Val.CompareTo(_cursor!.Val) == 0)
                    {
                        Reset();
                    }
                }
                else if (temp.Next == null)
                {
                    _tail = temp.Prev;
                    temp.Prev.Next = temp.Next;
                    
                    if (temp.Val.CompareTo(_cursor!.Val) == 0)
                    {
                        Reset();
                    }
                }
                else
                {
                    temp.Prev.Next = temp.Next;
                    temp.Next.Prev = temp.Prev;
                    
                    if (temp.Val.CompareTo(_cursor!.Val) == 0)
                    {
                        Reset();
                    }
                }

                --_count;
                return;
            }
            
            temp = temp.Next;
        }

        throw new RemoveException("There is no such element in the collection.");
        
    }

    public T RemoveCurrent()
    {
        
        if (_cursor == null) throw new InvalidOperationException("Zero elements in Cursor.");
        
        var cursorVal = _cursor.Val; 
        
        if (_cursor.Prev == null && _cursor.Next == null)
        {
            _head = null;
            _tail = null;
                    
            Reset();
        }
        else if (_cursor.Prev == null)
        {
            _head = _cursor.Next;
            _cursor!.Next!.Prev = _cursor.Prev;
                    
            if (_cursor.Val.CompareTo(_cursor.Val) == 0)
            {
                Reset();
            }
        }
        else if (_cursor.Next == null)
        {
            _tail = _cursor.Prev;
            _cursor.Prev.Next = _cursor.Next;
                    
            if (_cursor.Val.CompareTo(_cursor.Val) == 0)
            {
                Reset();
            }
        }
        else
        {
            _cursor.Prev.Next = _cursor.Next;
            _cursor.Next.Prev = _cursor.Prev;
                    
            if (_cursor.Val.CompareTo(_cursor.Val) == 0)
            {
                Reset();
            }
        }

        --_count;
        
        return cursorVal;
    }


    public void Print()
    {
        var temp = _head;
        while (temp != null)
        {
            Console.WriteLine(temp.Val);
            temp = temp.Next;
        }
    }
    
    
    // // public IEnumerator IEnumerable<T>.GetEnumerator()
    // // {
    // //     return new CollectionEnum(_head);
    // // }
    //
    // IEnumerator GetEnumerator()
    // {
    //     return new CollectionEnum(_head);
    // }
    
    // Enumerator class
    private class CollectionEnum : IEnumerator<T>
    {
        private readonly Node? _head;
        
        private Node? _current;
        
        
        object IEnumerator.Current
        {
            get
            {
                if (_current == null) throw new InvalidOperationException("Zero elements in collection. Try to call MoveNext.");
                return _current.Val;
            }
        }

        public T Current
        {
            get
            {
                if (_current == null) throw new InvalidOperationException("Zero elements in collection. Try to call MoveNext.");
                return _current.Val;
            }
        }

        public CollectionEnum(Node? head)
        {
            _head = head;
            _current = _head;
        }

        public void Reset()
        {
            _current = _head;
        }
        
        public bool MoveNext()
        {
            if (_current?.Next == null) return false;
            _current = _current.Next;
            return true;
        }
        
        public void Dispose()
        {
            // ??
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CollectionEnum(_head);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}