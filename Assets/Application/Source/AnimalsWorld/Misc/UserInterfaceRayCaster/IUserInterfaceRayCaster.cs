using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimalsWorld
{
   public interface IUserInterfaceRayCaster
   {
      void Initialize();

      void Project(Vector2 position, out List<RaycastResult> raycastResults);
   }
}