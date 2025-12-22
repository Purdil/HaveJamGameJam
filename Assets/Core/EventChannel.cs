using System;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "EventChannel", menuName = "EventChannel", order = 0)]
    public abstract class EventChannel<T> : ScriptableObject
    {
        public event Action<T> OnEvent;

        public void Raise(T obj)
        {
            OnEvent?.Invoke(obj);
        }
    }
}