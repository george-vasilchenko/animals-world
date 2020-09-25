using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class SideItemBasedScrollTrigger : ScrollTriggerBase
   {
      public override event OnScrollTriggerDelegate OnTriggered;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;
      public IScrollController ScrollController => _scrollController;
      public IScrollItemSelector ScrollItemSelector => _scrollItemSelector;

      private IScrollItem _currentScrollItem;
      private IScrollItem _lhsScrollItem;
      private IScrollItem _rhsScrollItem;

      [SerializeField] private ScrollItemsCollection _scrollItemsCollection = default;
      [SerializeField] private ScrollController _scrollController = default;
      [SerializeField] private ScrollItemSelector _scrollItemSelector = default;

      #region Unity API

      public void Awake()
      {
         if (ScrollController == null ||
             ScrollItemsCollection == null ||
             ScrollItemSelector == null)
         {
            throw new Exception("Not all dependencies were resolved.");
         }
      }

      public void OnEnable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged += OnTargetValueExternallyChanged;
         }

         if (ScrollItemSelector != null)
         {
            ScrollItemSelector.OnScrollItemSelected += ScrollItemSelector_OnScrollItemSelected;
         }
      }

      public void OnDisable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged -= OnTargetValueExternallyChanged;
         }

         if (ScrollItemSelector != null)
         {
            ScrollItemSelector.OnScrollItemSelected -= ScrollItemSelector_OnScrollItemSelected;
         }
      }

      #endregion Unity API

      private void ScrollItemSelector_OnScrollItemSelected(OnScrollItemSelectedEventArgs args)
      {
         if (args.ScrollItem == _currentScrollItem)
         {
            return;
         }

         var changeDirection = args.ScrollItem == _lhsScrollItem ? 1 : -1;
         OnTriggered?.Invoke(changeDirection);
      }

      public override void OnTargetValueExternallyChanged(int newValue)
      {
         var leftItemIndex = newValue - 1;
         var rightItemIndex = newValue + 1;

         _lhsScrollItem = leftItemIndex >= 0
            ? ScrollItemsCollection.GetScrollItemAtIndex(leftItemIndex)
            : null;
         _rhsScrollItem = rightItemIndex <= ScrollItemsCollection.CollectionLength - 1
            ? ScrollItemsCollection.GetScrollItemAtIndex(rightItemIndex)
            : null;
         _currentScrollItem = ScrollItemsCollection.GetScrollItemAtIndex(newValue);
      }
   }
}