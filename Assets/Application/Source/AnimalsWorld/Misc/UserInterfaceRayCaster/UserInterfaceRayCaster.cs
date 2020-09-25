using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class UserInterfaceRayCaster : MonoBehaviour, IUserInterfaceRayCaster
   {
      public GraphicRaycaster GraphicRaycaster;
      public EventSystem EventSystem;

      private PointerEventData _raycastPointerEventData;
      private bool _isInitialized;

      public void Initialize()
      {
         if (GraphicRaycaster == null)
         {
            throw new Exception("Graphic Raycaster must not be null.");
         }

         if (EventSystem == null)
         {
            throw new Exception("Event System must not be null.");
         }

         _raycastPointerEventData = new PointerEventData(EventSystem);

         _isInitialized = true;
      }

      public void Project(Vector2 position, out List<RaycastResult> raycastResults)
      {
         if (!_isInitialized)
         {
            throw new Exception("UiRaycaster is not initialzied.");
         }

         raycastResults = new List<RaycastResult>();

         _raycastPointerEventData.position = position;

         GraphicRaycaster.Raycast(_raycastPointerEventData, raycastResults);

         _raycastPointerEventData.Reset();
      }
   }
}