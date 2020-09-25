using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class ItemPositionBasedScrollTrigger : ScrollTriggerWithSwipeBase
   {
      public Canvas Canvas;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      public IScrollController ScrollController => _scrollController;

      public override ISwipeHandler SwipeHandler => _swipeHandler;

      public override event OnScrollTriggerDelegate OnTriggered;

      [SerializeField]
      private SwipeHandler _swipeHandler = default;

      [SerializeField]
      private ScrollController _scrollController = default;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      private RectTransform _centeRectTransform;
      private float _closestDistanceFromCenterToAnElement;
      private float[] _distancesFromCollectionElementsToCenterRectTransform;
      private int _currentClosestToCenterElementIndex;
      private int _lastClosestToCenterElementIndex;

      #region Unity API

      public void OnEnable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipePointerUp += SwipeHandler_OnSwipePointerUp;
         }

         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged +=
               OnTargetValueExternallyChanged;
         }
      }

      public void OnDisable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnSwipePointerUp -= SwipeHandler_OnSwipePointerUp;
         }

         if (ScrollController != null)
         {
            ScrollController.OnScrollControllerCurrentIndexChanged -=
               OnTargetValueExternallyChanged;
         }
      }

      public void Awake()
      {
         if (ScrollController == null ||
             Canvas == null ||
             ScrollItemsCollection == null ||
             SwipeHandler == null)
         {
            throw new Exception("Not all dependencies were resolved.");
         }
      }

      public void Start()
      {
         _centeRectTransform = CreateCenterRectTransform();
         _distancesFromCollectionElementsToCenterRectTransform = new float[ScrollItemsCollection.CollectionLength];
      }

      public void Update()
      {
         if (SwipeHandler.IsPointerDown)
         {
            DragScrollItemsCollection();
            TrackScrollCollectionClosestItemToCenterChanges();
         }
      }

      #endregion Unity API

      public override void OnTargetValueExternallyChanged(int newValue) => _lastClosestToCenterElementIndex = newValue;

      private void SwipeHandler_OnSwipePointerUp()
      {
         var changeDirection = _lastClosestToCenterElementIndex - _currentClosestToCenterElementIndex;

         //Debug.LogFormat("last:{0}, new:{1}, dir:{2}",
         //   _lastClosestToCenterElementIndex, _currentClosestToCenterElementIndex, changeDirection);

         OnTriggered?.Invoke(changeDirection);

         _lastClosestToCenterElementIndex = _currentClosestToCenterElementIndex;
      }

      private RectTransform CreateCenterRectTransform()
      {
         var centerGameObject = new GameObject("Center", typeof(RectTransform));
         var centerRectTransform = centerGameObject.GetComponent<RectTransform>();

         centerRectTransform.SetParent(transform, false);
         centerRectTransform.anchoredPosition = Vector2.zero;
         centerRectTransform.pivot = new Vector2(0.5f, 0.5f);
         centerRectTransform.localScale = Vector3.zero;

         return centerRectTransform;
      }

      private void TrackScrollCollectionClosestItemToCenterChanges()
      {
         for (var i = 0; i < ScrollItemsCollection.CollectionLength; i++)
         {
            _distancesFromCollectionElementsToCenterRectTransform[i] =
               Mathf.Abs(_centeRectTransform.position.x - ScrollItemsCollection.GetElementWorldPositionAtIndex(i).x);
         }

         _closestDistanceFromCenterToAnElement = Mathf.Min(_distancesFromCollectionElementsToCenterRectTransform);

         for (var i = 0; i < ScrollItemsCollection.CollectionLength; i++)
         {
            if (Mathf.Abs(_closestDistanceFromCenterToAnElement - _distancesFromCollectionElementsToCenterRectTransform[i]) <= 0.01f)
            {
               _currentClosestToCenterElementIndex = i;
            }
         }
      }

      private void DragScrollItemsCollection()
      {
         var amount = SwipeHandler.SwipeDeltaHorizontal * SwipeHandler.SwipeCurrentDirection.x / Canvas.scaleFactor;
         var currentCollectionPosition = ScrollItemsCollection.GetAnchoredPosition();

         var targetPosition = new Vector2(currentCollectionPosition.x + amount, currentCollectionPosition.y);
         ScrollItemsCollection.SetAnchoredPosition(targetPosition);
      }
   }
}