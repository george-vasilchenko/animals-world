using UnityEngine;

namespace AnimalsWorld
{
   public interface ISwipeHandler
   {
      event OnPointerUpSwipeDelegate OnPointerUpSwipe;

      event OnSwipePointerDownDelegate OnSwipePointerDown;

      event OnSwipePointerUpDelegate OnSwipePointerUp;

      event OnSwipeClickDelegate OnSwipeClick;

      bool IsSwiping { get; }

      bool IsPointerDown { get; }

      Vector2 SwipeInitialDirection { get; }

      Vector2 SwipeCurrentDirection { get; }

      float SwipeDeltaHorizontal { get; }

      float SwipePhaseDistanceHorizontal { get; }
   }
}