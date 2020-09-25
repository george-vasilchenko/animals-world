using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   [CreateAssetMenu(menuName = "AnimalsWorld/LevelScroll/Events/CurrentScrollItemIndexChangedEvent", order = 1)]
   public class CurrentScrollItemIndexChangedEvent : ScriptableObject
   {
      private readonly List<ICurrentScrollItemIndexChangedEventListener> _listeners = new List<ICurrentScrollItemIndexChangedEventListener>();

      public void RegisterListener(ICurrentScrollItemIndexChangedEventListener listener)
      {
         if (!_listeners.Contains(listener))
         {
            _listeners.Add(listener);
         }
      }

      public void RemoveListener(ICurrentScrollItemIndexChangedEventListener listener)
      {
         if (_listeners.Contains(listener))
         {
            _listeners.Remove(listener);
         }
      }

      public void Invoke(int currentIndex)
      {
         if (_listeners.Count <= 0)
         {
            return;
         }

         for (var i = _listeners.Count - 1; i >= 0; i--)
         {
            var listener = _listeners[i];
            listener.OnCurrentScrollItemIndexChangedHandler(currentIndex);
         }
      }
   }
}