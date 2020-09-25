using UnityEngine;

namespace AnimalsWorld
{
   public abstract class Element : MonoBehaviour, IElement
   {
      public virtual void Hide()
      {
         if (!gameObject.activeSelf)
         {
            return;
         }

         gameObject.SetActive(false);
      }

      public virtual void Show()
      {
         if (gameObject.activeSelf)
         {
            return;
         }

         gameObject.SetActive(true);
      }
   }
}