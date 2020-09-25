using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [Serializable]
   public struct SwipeHandlerData
   {
      [SerializeField]
#pragma warning disable 649
      private int _clickSensitivity;

#pragma warning restore 649

      public Vector3 LastMousePosition { get; set; }

      public float ClickSensitivity => _clickSensitivity;

      public void Initialize(GameObject parentGameObject)
      {
         if (parentGameObject == null)
         {
            throw new Exception("Parent game object must not be null.");
         }

         var image = parentGameObject.GetComponent<Image>();

         if (image == null)
         {
            throw new Exception("Image component must not be null.");
         }
      }
   }
}