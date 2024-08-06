using UnityEngine;
using System.Collections.Generic;

namespace com
{
    public class BetterEvent
    {
        private List<System.Action> _list;

        public BetterEvent(int prealocate = 10)
        {
            _list = new List<System.Action>(prealocate);
        }

        public void Add(System.Action callback)
        {
            _list.Add(callback);
        }

        public void Remove(System.Action callback)
        {
            _list.Remove(callback);
        }

        public void Invoke()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Invoke();
            }
        }
    }

    public class BetterEvent<T>
    {
        private List<System.Action<T>> _list;
        public BetterEvent(int prealocate = 10)
        {
            _list = new List<System.Action<T>>(prealocate);
        }

        public void Add(System.Action<T> callback)
        {
            _list.Add(callback);
        }

        public void Remove(System.Action<T> callback)
        {
            _list.Remove(callback);
        }

        public void Invoke(T args)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].Invoke(args);
            }
        }

        public bool Contains(System.Action<T> callback)
        {
            return _list.Contains(callback);
        }
    }
}
