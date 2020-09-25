using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class SwipeDistanceBasedScrollTrigger : ScrollTriggerWithSwipeBase
   {
      public Canvas Canvas;

      public IScrollItemsCollection ScrollItemsCollection => _scrollItemsCollection;

      public override ISwipeHandler SwipeHandler => _swipeHandler;

      public override event OnScrollTriggerDelegate OnTriggered;

      [SerializeField]
      private SwipeHandler _swipeHandler = default;

      [SerializeField]
      private ScrollItemsCollection _scrollItemsCollection = default;

      private float _minimalHorizontalSwipeDeltaForSingleTransition;

      #region Unity API

      public void Awake()
      {
         if (SwipeHandler == null ||
             ScrollItemsCollection == null)
         {
            throw new Exception("Not all dependencies were resolved.");
         }
      }

      public void OnEnable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnPointerUpSwipe += SwipeHandler_OnPointerUpSwipeDelta;
         }
      }

      public void OnDisable()
      {
         if (SwipeHandler != null)
         {
            SwipeHandler.OnPointerUpSwipe -= SwipeHandler_OnPointerUpSwipeDelta;
         }
      }

      public void Update()
      {
         if (SwipeHandler.IsPointerDown)
         {
            DragScrollItemsCollection();
         }
      }

      public void Start()
      {
         _minimalHorizontalSwipeDeltaForSingleTransition = (float)Screen.width / 10;
         _minimalHorizontalSwipeDeltaForSingleTransition /= Canvas.scaleFactor;
      }

      #endregion Unity API

      public override void OnTargetValueExternallyChanged(int newValue)
      {
         throw new NotImplementedException();
      }

      private void SwipeHandler_OnPointerUpSwipeDelta(OnPointerUpSwipeEventArgs args)
      {
         // decide if the distance of swipe is sufficient to make trigger the transition
         if (args.SwipePhaseDistance.x < _minimalHorizontalSwipeDeltaForSingleTransition)
         {
            return;
         }

         // calculate amount of indices to transition and their direction (-2, -1, 1, 2), there is no
         // 0 here, amount is ceiled to 1
         var value = (int)(args.SwipeDelta.x / _minimalHorizontalSwipeDeltaForSingleTransition);
         value = value == 0 || value == 1 ? 1 : value;
         value *= (int)args.SwipeDirection.x;

         //Debug.LogFormat("original: {0}, converted: {1}", args.SwipeDelta, value);
         OnTriggered?.Invoke(value);
      }

      private void DragScrollItemsCollection()
      {
         const float denominator = 0.5f;
         var amount = SwipeHandler.SwipeDeltaHorizontal
                      * SwipeHandler.SwipeCurrentDirection.x
                      / Canvas.scaleFactor
                      * denominator;

         var currentCollectionPosition = ScrollItemsCollection.GetAnchoredPosition();
         var targetPosition = new Vector2(currentCollectionPosition.x + amount, currentCollectionPosition.y);

         ScrollItemsCollection.SetAnchoredPosition(targetPosition);
      }
   }
}