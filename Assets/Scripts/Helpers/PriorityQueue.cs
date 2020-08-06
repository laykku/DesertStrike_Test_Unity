using System.Collections.Generic;

namespace DesertStrike.Helpers
{
    public class PriorityQueueItem<T>
    {
        public readonly T Value;
        public readonly double Priority;

        public PriorityQueueItem(T value, double priority)
        {
            Value = value;
            Priority = priority;
        }
    }
    
    public class PriorityQueue<T>
    {
        private readonly List<PriorityQueueItem<T>> _items = new List<PriorityQueueItem<T>>();

        public int Count => _items.Count;
        
        public void Enqueue(T value, double priority)
        {
            _items.Add(new PriorityQueueItem<T>(value, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;
            
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Priority < _items[bestIndex].Priority)
                {
                    bestIndex = i;
                }
            }

            T value = _items[bestIndex].Value;
            _items.RemoveAt(bestIndex);
            return value;
        }
    }
}