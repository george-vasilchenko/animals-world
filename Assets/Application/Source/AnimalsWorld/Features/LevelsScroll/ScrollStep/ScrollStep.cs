using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimalsWorld
{
   public abstract class ScrollStep : MonoBehaviour, IPointerClickHandler
   {
      protected int Index;
      protected Action<int> ClickCallback;
      protected bool IsEnabled;
      protected bool IsDone;

      public abstract void UpdateState();

      public virtual void Initialize(int index, Action<int> clickCallback)
      {
         Index = index;
         ClickCallback = clickCallback;
      }

      public abstract void SetEnabled(bool isEnabled, bool forceInstant = false);

      public abstract void OnPointerClick(PointerEventData eventData);
   }
}