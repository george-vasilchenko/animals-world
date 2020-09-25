using MalenkiyApps;
using UnityEngine;
using UnityEngine.Events;

namespace AnimalsWorld
{
   public class ScrollItem : MonoBehaviour, IScrollItem
   {
      public UnityEvent OnClickEvent;
      private int _itemIndex;

      public virtual void OnClick()
      {
         Validation.Run(() => _itemIndex >= 0, "_itemIndex", "Must be greater than or equal 0.");

         OnClickEvent?.Invoke();
      }

      public void SetIndex(int index) => _itemIndex = index;

      string IScrollItem.ItemName => gameObject.name;
      int IScrollItem.ItemIndex => _itemIndex;
   }
}