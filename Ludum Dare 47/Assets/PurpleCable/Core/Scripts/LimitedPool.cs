using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PurpleCable
{
    public abstract class LimitedPool<TPoolable> : MonoBehaviour
        where TPoolable : IPoolable
    {
        private Queue<TPoolable> _queue;

        [SerializeField]
        private int MaxCapacity = 100;

        private void Awake()
        {
            _queue = new Queue<TPoolable>(MaxCapacity);

            for (int i = 0; i < MaxCapacity; i++)
            {
                _queue.Enqueue(CreateItem());
            }
        }

        protected abstract TPoolable CreateItem();

        public TPoolable GetItem()
        {
            TPoolable item = _queue.Dequeue();

            _queue.Enqueue(item);

            item.SetAsInUse();

            return item;
        }
    }
}
