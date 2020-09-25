using System.Collections.Generic;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimalsWorld
{
   public class ScrollItemSelector : MonoBehaviour, IScrollItemSelector
   {
      public event OnScrollItemSelectedDelegate OnScrollItemSelected;

      public ISwipeHandler SwipeHandler => _swipeHandler;

      public IScrollController ScrollController => _scrollController;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      [SerializeField]
      private SwipeHandler _swipeHandler = default;

      [SerializeField]
      private ScrollController _scrollController = default;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      private int _currentSelectedItemIndex;

      public void Awake()
      {
         Validation.Run(() => ScrollItemsCollection != null, "ScrollItemsCollection", "Must not be null.");
         Validation.Run(() => ScrollController != null, "ScrollController", "Must not be null.");
         Validation.Run(() => SwipeHandler != null, "SwipeHandler", "Must not be null.");
      }

      public void OnEnable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipeClick += SwipeHandler_OnSwipeClick;
         }

         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged += ScrollController_OnScrollControllerCurrentIndexChanged;
         }
      }

      public void OnDisable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipeClick -= SwipeHandler_OnSwipeClick;
         }

         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged -= ScrollController_OnScrollControllerCurrentIndexChanged;
         }
      }

      private void ScrollController_OnScrollControllerCurrentIndexChanged(int newIndex) => _currentSelectedItemIndex = newIndex;

      private void SwipeHandler_OnSwipeClick(List<RaycastResult> resultsList)
      {
         if (resultsList.Count <= 0)
         {
            return;
         }

         foreach (var result in resultsList)
         {
            var scrollItem = (IScrollItem)result.gameObject.GetComponent<ScrollItem>();

            if (scrollItem == null)
            {
               continue;
            }

            if (scrollItem.ItemIndex == _currentSelectedItemIndex)
            {
               scrollItem.OnClick();
            }

            OnScrollItemSelected?.Invoke(new OnScrollItemSelectedEventArgs(scrollItem));
         }

         resultsList.Clear();
      }
   }
}