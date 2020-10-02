using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PurpleCable
{
    public interface IPoolable
    {
        bool IsInUse { get; }

        void SetAsInUse();

        void SetAsAvailable();
    }
}
