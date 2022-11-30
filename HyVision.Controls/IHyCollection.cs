using System.Collections;

namespace HyVision
{
    public interface IHyCollection<T> : IEnumerable
    {
        T this[int index] { get; }

        T this[string key] { get; }

        int Count { get; }

        bool Contains(string key);

        void Add(T value);

        void Insert(int index, T value);

        bool Remove(string key);

        void RemoveAt(int index);

        void Clear();

        bool MoveUp(int index);

        bool MoveDown(int index);
    }
}
