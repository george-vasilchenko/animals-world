using System.Collections.Generic;
using UnityEngine;

namespace AnimalsWorld
{
   public class CatchFallingObjectContainer : MonoBehaviour
   {
      private List<GameObject> _fallingObjects;

      public void Add(GameObject obj)
      {
         _fallingObjects.Add(obj);
         obj.transform.SetParent(transform, true);
      }

      public void Clean()
      {
         foreach (var o in _fallingObjects)
         {
            if (o != null)
            {
               Destroy(o);
            }
         }
      }

      public void Setup() => _fallingObjects = new List<GameObject>();
   }
}