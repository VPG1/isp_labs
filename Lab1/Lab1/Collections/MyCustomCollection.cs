using System.Collections;
using System.Collections.Specialized;

namespace Lab1.Collections;

using Interfaces;

public class MyCustomCollection<T> : IEnumerable<T>, ICustomCollection<T> where T : IComparable
{
    private class Node
    {
        public T val;
        public Node? next = null;
        public Node? prev = null;


        public Node(T val, Node? next, Node? prev)
        {
            this.val = val;
            this.next = next;
            this.prev = prev;
        }
    }

    

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return new CollectionEnum(_head);
    }

    public IEnumerator GetEnumerator()
    {
        return new CollectionEnum(_head);
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
                temp = temp.next;
            }

            return temp.val;
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
                _head = _head.next;
            }

            _head.val = value;

        }
    }
    
    public void Reset()
    {
        _cursor = _head;
    }

    public void Next()
    {
        if (_cursor is null) throw new IndexOutOfRangeException("zero elements in the collection");
        
        _cursor = _cursor.next ?? throw new IndexOutOfRangeException("cursor on the last element of the collection");
    }

    public T Current()
    {
        if (_cursor is null) throw new NullReferenceException("zero elements in the collection");
        
        return _cursor.val;
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
            if (temp.val.CompareTo(item) == 0)
            {
                throw new InvalidOperationException("this item is already in the collection");
            }

            temp = temp.next;
        }
        
        // добавляем в конец
        _tail.next = new Node(item, null, _tail);
        _tail = _tail.next;

        ++_count;
    }

    public void Remove(T item)
    {
        #nullable disable

        var temp = _head;
        while (temp != null)
        {
            if (temp.val.CompareTo(item) == 0)
            {
                if (temp.prev == null && temp.next == null)
                {
                    _head = null;
                    _tail = null;
                    
                    Reset();
                }
                else if (temp.prev == null)
                {
                    _head = temp.next;
                    temp.next.prev = temp.prev;
                    
                    if (temp.val.CompareTo(_cursor.val) == 0)
                    {
                        Reset();
                    }
                }
                else if (temp.next == null)
                {
                    _tail = temp.prev;
                    temp.prev.next = temp.next;
                    
                    if (temp.val.CompareTo(_cursor.val) == 0)
                    {
                        Reset();
                    }
                }
                else
                {
                    temp.prev.next = temp.next;
                    temp.next.prev = temp.prev;
                    
                    if (temp.val.CompareTo(_cursor.val) == 0)
                    {
                        Reset();
                    }
                }

                --_count;
                return;
            }
            
            temp = temp.next;
        }
        --_count;

#nullable restore
    }

    public T RemoveCurrent()
    {
        
        if (_cursor == null) throw new InvalidOperationException("zero elements in Cursor");
        
        var cursorVal = _cursor.val; 
        
        if (_cursor.prev == null && _cursor.next == null)
        {
            _head = null;
            _tail = null;
                    
            Reset();
        }
        else if (_cursor.prev == null)
        {
            _head = _cursor.next;
            _cursor.next.prev = _cursor.prev;
                    
            if (_cursor.val.CompareTo(_cursor.val) == 0)
            {
                Reset();
            }
        }
        else if (_cursor.next == null)
        {
            _tail = _cursor.prev;
            _cursor.prev.next = _cursor.next;
                    
            if (_cursor.val.CompareTo(_cursor.val) == 0)
            {
                Reset();
            }
        }
        else
        {
            _cursor.prev.next = _cursor.next;
            _cursor.next.prev = _cursor.prev;
                    
            if (_cursor.val.CompareTo(_cursor.val) == 0)
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
            Console.WriteLine(temp.val);
            temp = temp.next;
        }
    }
    
    
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
                return _current.val;
            }
        }

        public T Current
        {
            get
            {
                if (_current == null) throw new InvalidOperationException("Zero elements in collection. Try to call MoveNext.");
                return _current.val;
            }
        }

        public CollectionEnum(Node? head)
        {
            this._head = head;
        }

        public void Reset()
        {
            _current = _head;
        }
        
        public bool MoveNext()
        {
            if (_current?.next == null) return false;
            _current = _current.next;
            return true;
        }
        
        public void Dispose()
        {
            // ??
        }
    }
}