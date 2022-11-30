using System;

namespace HyVision.Models
{
    [Serializable]
    public abstract class BaseHyCollection
    {
        public event EventHandler<CollectionItemEventArgs> Inserted;

        public event EventHandler<CollectionItemEventArgs> Removed;

        public event EventHandler<CollectionItemEventArgs> ItemValueChanged;

        public event EventHandler<CollectionItemMoveEventArgs> MovedItem;

        public event EventHandler Cleared;

        internal event EventHandler<HyExceptionEventArgs> Exception;

        protected virtual void OnInserted(int index, object value)
        {
            Inserted?.Invoke(this, new CollectionItemEventArgs(index, value));
        }

        protected virtual void OnRemoved(int index, object value)
        {
            Removed?.Invoke(this, new CollectionItemEventArgs(index, value));
        }

        protected virtual void OnItemValueChanged(int index, object value)
        {
            ItemValueChanged?.Invoke(this, new CollectionItemEventArgs(index, value));
        }

        protected virtual void OnMoved(int fromIndex, int toIndex)
        {
            MovedItem?.Invoke(this, new CollectionItemMoveEventArgs(fromIndex, toIndex));
        }

        protected virtual void OnCleared()
        {
            Cleared?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnException(object sender, HyExceptionEventArgs e)
        {
            Exception?.Invoke(sender, e);
        }
    }
}
