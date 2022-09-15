using System.Collections.Specialized;

namespace Lab1.Collections;

using Interfaces;

public class MyCustomCollection<T> : ICustomCollection<T> where T : IComparable
{
    class Node
    {
        public T value;
        public Node? next = null;
        public Node? prev = null;
    }

    private Node? head = null;
    private Node? tail = null;
    private Node? cursor = null;
    private int count = 0;

    public T this[int index]
    {
        get
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException("out of range");
            }
            
            var temp = head;
            
            while (index != 0)
            {
                --index;
                temp = temp.next;
            }

            return temp.value;
        }
        set
        {
            
            if (index >= Count)
            {
                throw new IndexOutOfRangeException("out of range");
            }
            
            var temp = head;
            
            while (count != 0)
            {
                --count;
                head = head.next;
            }

            head.value = value;

        }
    }
    
    public void Reset()
    {
        cursor = head;
    }

    public void Next()
    {
        if (cursor is null) throw new IndexOutOfRangeException("zero elements in the collection");
        
        cursor = cursor.next ?? throw new IndexOutOfRangeException("cursor on the last element of the collection");
    }

    public T Current()
    {
        if (cursor is null) throw new NullReferenceException("zero elements in the collection");
        
        return cursor.value;
    }

    public int Count
    {
        get
        {
            return count;
        }
    }

    public void Add(T item)
    {
        if (tail == null) // если в коллекции ноль элементов
        {
            head = new Node {value = item, next = null, prev = null};
            tail = head;
            cursor = head;
            ++count;
            return;
        }
        
        // проверяем нет ли в коллекции такого же элемента
        var temp = head;
        while (temp != null)
        {
            if (temp.value.CompareTo(item) == 0)
            {
                throw new InvalidOperationException("this item is already in the collection");
            }

            temp = temp.next;
        }
        
        // добавляем в конец
        tail.next = new Node {value = item, next = null, prev = tail};
        tail = tail.next;

        ++count;
    }

    public void Remove(T item)
    {
        #nullable disable

        var temp = head;
        while (temp != null)
        {
            if (temp.value.CompareTo(item) == 0)
            {
                if (temp.prev == null && temp.next == null)
                {
                    head = null;
                    tail = null;
                    
                    Reset();
                }
                else if (temp.prev == null)
                {
                    head = temp.next;
                    temp.next.prev = temp.prev;
                    
                    if (temp.value.CompareTo(cursor.value) == 0)
                    {
                        Reset();
                    }
                }
                else if (temp.next == null)
                {
                    tail = temp.prev;
                    temp.prev.next = temp.next;
                    
                    if (temp.value.CompareTo(cursor.value) == 0)
                    {
                        Reset();
                    }
                }
                else
                {
                    temp.prev.next = temp.next;
                    temp.next.prev = temp.prev;
                    
                    if (temp.value.CompareTo(cursor.value) == 0)
                    {
                        Reset();
                    }
                }

                --count;
                return;
            }
            
            temp = temp.next;
        }
        --count;

#nullable restore
    }

    public T RemoveCurrent()
    {
        
        if (cursor == null) throw new InvalidOperationException("zero elements in Cursor");
        
        var cursorVal = cursor.value; 
        
        if (cursor.prev == null && cursor.next == null)
        {
            head = null;
            tail = null;
                    
            Reset();
        }
        else if (cursor.prev == null)
        {
            head = cursor.next;
            cursor.next.prev = cursor.prev;
                    
            if (cursor.value.CompareTo(cursor.value) == 0)
            {
                Reset();
            }
        }
        else if (cursor.next == null)
        {
            tail = cursor.prev;
            cursor.prev.next = cursor.next;
                    
            if (cursor.value.CompareTo(cursor.value) == 0)
            {
                Reset();
            }
        }
        else
        {
            cursor.prev.next = cursor.next;
            cursor.next.prev = cursor.prev;
                    
            if (cursor.value.CompareTo(cursor.value) == 0)
            {
                Reset();
            }
        }

        --count;
        
        return cursorVal;
    }


    public void Print()
    {
        var temp = head;
        while (temp != null)
        {
            Console.WriteLine(temp.value);
            temp = temp.next;
        }
    }
}