using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   public class BaloonContainer : MonoBehaviour
   {
      private List<GameObject> _baloons;

      public void Add(GameObject obj)
      {
         _baloons.Add(obj);
         obj.transform.SetParent(transform, true);
      }

      public void Clean()
      {
         foreach (var o in _baloons)
         {
            if (o != null)
            {
               Destroy(o);
            }
         }
      }

      public void Setup() => _baloons = new List<GameObject>();
   }
}