using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreServer.CommonServices
{
    public class EventAggregator
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            if (!_subscribers.ContainsKey(typeof(T)))
            {
                _subscribers[typeof(T)] = new List<Delegate>();
            }
            _subscribers[typeof(T)].Add(handler);
        }

        public void Publish<T>(T eventMessage)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                foreach (var handler in _subscribers[typeof(T)])
                {
                    ((Action<T>)handler)?.Invoke(eventMessage);
                }
            }
        }
    }
}
