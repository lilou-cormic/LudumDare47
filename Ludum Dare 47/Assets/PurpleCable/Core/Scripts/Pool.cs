using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public abstract class Pool<TPoolable> : MonoBehaviour
        where TPoolable : IPoolable
    {
        private List<TPoolable> _list;

        [SerializeField]
        private int BatchCount = 10;

        protected virtual void Awake()
        {
            _list = new List<TPoolable>(BatchCount);
        }

        protected abstract TPoolable CreateItem();

        public TPoolable GetItem()
        {
            TPoolable item = _list.FirstOrDefault(x => !x.IsInUse);

            if (item == null)
            {
                for (int i = 0; i < BatchCount; i++)
                {
                    var newItem = CreateItem();
                    newItem.SetAsAvailable();

                    _list.Add(newItem);
                }

                item = _list.FirstOrDefault(x => !x.IsInUse);
            }

            item.SetAsInUse();

            return item;
        }
    }
}
