using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class ScrollItemScaler : MonoBehaviour
   {
      public IScrollController ScrollController => _scrollController;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      public float ScaleIncrement = 1.25f;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      [SerializeField]
      private ScrollController _scrollController = default;

      private Vector2 _originalItemScale;
      private Vector2 _targetItemScale;
      private bool _isDoneResizingCurrentItem;
      private int _currentResizableItemIndex;

      public void Awake()
      {
         Validation.Run(() => ScrollController != null, "ScrollController", "Must not be null.");
         Validation.Run(() => ScrollItemsCollection != null, "ScrollItemsCollection", "Must not be null.");
      }

      public void OnEnable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged += ScrollController_OnScrollControllerCurrentIndexChanged;
         }

         _isDoneResizingCurrentItem = false;
      }

      public void OnDisable()
      {
         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged -= ScrollController_OnScrollControllerCurrentIndexChanged;
         }
      }

      public void Start()
      {
         _originalItemScale = ScrollItemsCollection.GetElementScaleAtIndex(0);
         _targetItemScale = _originalItemScale * ScaleIncrement;
      }

      public void Update()
      {
         if (!_isDoneResizingCurrentItem)
         {
            ScaleCurrentItem();
         }
      }

      public void ForceResizeItemsForIndex(int index)
      {
         _currentResizableItemIndex = index;

         for (var i = 0; i < ScrollItemsCollection.CollectionLength; i++)
         {
            ScrollItemsCollection.SetElementScaleAtIndex(i, i == index ? _targetItemScale : _originalItemScale);
         }
      }

      private void ScrollController_OnScrollControllerCurrentIndexChanged(int newindex)
      {
         _currentResizableItemIndex = newindex;
         _isDoneResizingCurrentItem = false;
      }

      private void ScaleCurrentItem()
      {
         for (var i = 0; i < ScrollItemsCollection.CollectionLength; i++)
         {
            if (i == _currentResizableItemIndex)
            {
               var currentScale = ScrollItemsCollection.GetElementScaleAtIndex(i);
               var newScale = Vector2.Lerp(currentScale, _targetItemScale, Time.deltaTime * 10);

               ScrollItemsCollection.SetElementScaleAtIndex(i, newScale);

               _isDoneResizingCurrentItem = Vector2.Distance(ScrollItemsCollection.GetElementScaleAtIndex(i), _targetItemScale) < 0.002f;
            }
            else
            {
               var currentScale = ScrollItemsCollection.GetElementScaleAtIndex(i);
               var newScale = Vector2.Lerp(currentScale, _originalItemScale, Time.deltaTime * 10);

               ScrollItemsCollection.SetElementScaleAtIndex(i, newScale);
            }
         }
      }
   }
}