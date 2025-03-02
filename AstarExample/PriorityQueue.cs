using System.Collections.Generic;
using System;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();
    private Dictionary<T, int> positions = new Dictionary<T, int>();

    public int Count => heap.Count;

    public void Enqueue(T item)
    {
        heap.Add(item);
        int index = heap.Count - 1;
        positions[item] = index;
        HeapifyUp(index);
    }

    
    public T Dequeue()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");

        T root = heap[0];
        T last = heap[heap.Count - 1];

        positions.Remove(root);
        if (heap.Count > 1)
        {
            heap[0] = last;
            positions[last] = 0;
            HeapifyDown(0);
        }
        heap.RemoveAt(heap.Count - 1);

        return root;
    }

    


    public bool Contains(T item) => positions.ContainsKey(item);

    public T Get(T item)
    {
        if (positions.TryGetValue(item, out int index))
        {
            return heap[index];
        }
        throw new InvalidOperationException("Item not found in the queue");
    }

    public void Remove(T item)
    {
        if (positions.TryGetValue(item, out int index))
        {
            T last = heap[heap.Count - 1];
            heap[index] = last;
            positions[last] = index;
            heap.RemoveAt(heap.Count - 1);
            positions.Remove(item);

            if (index < heap.Count)
            {
                HeapifyUp(index);
                HeapifyDown(index);
            }
        }
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[parent].CompareTo(heap[index]) <= 0) break;

            Swap(parent, index);
            index = parent;
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = heap.Count - 1;
        while (true)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallest = index;

            if (leftChild <= lastIndex && heap[leftChild].CompareTo(heap[smallest]) < 0)
                smallest = leftChild;

            if (rightChild <= lastIndex && heap[rightChild].CompareTo(heap[smallest]) < 0)
                smallest = rightChild;

            if (smallest == index) break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
        positions[heap[i]] = i;
        positions[heap[j]] = j;
    }
}
