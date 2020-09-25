using System.Collections.Generic;
using System.Linq;
using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class ScrollController : MonoBehaviour, IScrollController
   {
      public event OnScrollControllerCurrentIndexChangedDelegate OnScrollControllerCurrentIndexChanged;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      public ISwipeHandler SwipeHandler => _swipeHandler;

      public IEnumerable<IScrollTrigger> ScrollTriggers => _scrollTriggers;

      private int _currentItemIndex;

      [SerializeField]
      private ScrollTriggerBase[] _scrollTriggers = default;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      [SerializeField]
      private SwipeHandler _swipeHandler = default;

      #region effect fields

      // smooth scrolling to index
      private bool _isPointerDown;

      private float _smoothedTargetPositionX;
      private bool _isDoneSmoothScrollingToIndex;
      private Vector3 _updatedCollectionSmoothedAnchoredPosition;
      private Vector3 _currentCollectionSmoothedAnchoredPosition;

      #endregion effect fields

      #region other fields

      private bool _mustStartFromBeginning;
      private bool _isFirstUpdateFrame;

      #endregion other fields

      #region Unity API

      public void Awake()
      {
         Validation.Run(() => ScrollItemsCollection != null, "ScrollItemsCollection", "Must not be null.");
         Validation.Run(() => ScrollTriggers.Any(), "ScrollTriggers", "Must not be empty.");
         Validation.Run(() => SwipeHandler != null, "SwipeHandler", "Must not be null.");
      }

      public void Start()
      {
         _currentItemIndex = 0;
         ScrollToIndexDirectly(_currentItemIndex);

         //InvokeRepeating("BroadcastState", 2.0f, 1);
      }

      public void Update()
      {
         if (!_isPointerDown)
         {
            if (!_isDoneSmoothScrollingToIndex)
            {
               ScrollToIndexSmooth(out _isDoneSmoothScrollingToIndex);
            }
         }

         if (_isFirstUpdateFrame)
         {
            _isFirstUpdateFrame = false;
            OnScrollControllerCurrentIndexChanged?.Invoke(_currentItemIndex);
         }
      }

      public void OnEnable()
      {
         _isFirstUpdateFrame = true;

         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipePointerDown += SwipeHandler_OnSwipePointerDown;
            SwipeHandler.OnSwipePointerUp += SwipeHandler_OnSwipePointerUp;
         }

         if (ScrollTriggers.Any())
         {
            foreach (var scrollTrigger in ScrollTriggers)
            {
               scrollTrigger.OnTriggered += ScrollTrigger_OnTriggered;
            }
         }
      }

      public void OnDisable()
      {
         _isFirstUpdateFrame = true;

         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipePointerDown -= SwipeHandler_OnSwipePointerDown;
            SwipeHandler.OnSwipePointerUp -= SwipeHandler_OnSwipePointerUp;
         }

         if (ScrollTriggers.Any())
         {
            foreach (var scrollTrigger in ScrollTriggers)
            {
               scrollTrigger.OnTriggered -= ScrollTrigger_OnTriggered;
            }
         }

         // Decide where to start from (for back-from-chapters-panel and back-from-level)
         if (_mustStartFromBeginning)
         {
            _currentItemIndex = 0;
            OnScrollControllerCurrentIndexChanged?.Invoke(_currentItemIndex);

            ScrollToIndexDirectly(_currentItemIndex);
            _mustStartFromBeginning = false;
         }
      }

      #endregion Unity API

      #region Event handlers

      private void ScrollTrigger_OnTriggered(int targetItemIndexChangeDirection)
      {
         //Debug.LogFormat("ScrollController, OnTriggered, {0}", targetItemIndexChangeDirection);

         _currentItemIndex = Mathf.Clamp(
            _currentItemIndex - targetItemIndexChangeDirection, 0, ScrollItemsCollection.CollectionLength - 1);

         OnScrollControllerCurrentIndexChanged?.Invoke(_currentItemIndex);

         ResetSmoothScrollData();
      }

      private void SwipeHandler_OnSwipePointerUp()
      {
         _isPointerDown = false;
         ResetSmoothScrollData();
      }

      private void SwipeHandler_OnSwipePointerDown() => _isPointerDown = true;

      #endregion Event handlers

      public void SetMustStartFromBeginning() => _mustStartFromBeginning = true;

      private void ScrollToIndexDirectly(int index)
      {
         var targetXPosition = ScrollItemsCollection.GetElementLocalPositionAtIndex(index).x * -1;
         var currentCollectionPosition = ScrollItemsCollection.GetAnchoredPosition();

         ScrollItemsCollection.SetAnchoredPosition(new Vector2(targetXPosition, currentCollectionPosition.y));
      }

      private void ScrollToIndexSmooth(out bool isDone)
      {
         _smoothedTargetPositionX = ScrollItemsCollection.GetElementLocalPositionAtIndex(_currentItemIndex).x * -1;
         _currentCollectionSmoothedAnchoredPosition = ScrollItemsCollection.GetAnchoredPosition();

         _updatedCollectionSmoothedAnchoredPosition.x = Mathf.Lerp(_currentCollectionSmoothedAnchoredPosition.x,
            _smoothedTargetPositionX, Time.deltaTime * 10);
         _updatedCollectionSmoothedAnchoredPosition.y = _currentCollectionSmoothedAnchoredPosition.y;
         _updatedCollectionSmoothedAnchoredPosition =
            ScrollItemsCollection.SetAnchoredPosition(_updatedCollectionSmoothedAnchoredPosition);

         isDone = Mathf.Abs(Mathf.Abs(_smoothedTargetPositionX) -
                            Mathf.Abs(_updatedCollectionSmoothedAnchoredPosition.x)) <= 0.05f;
      }

      private void ResetSmoothScrollData() => _isDoneSmoothScrollingToIndex = false;

      private void BroadcastState() => OnScrollControllerCurrentIndexChanged?.Invoke(_currentItemIndex);
   }
}