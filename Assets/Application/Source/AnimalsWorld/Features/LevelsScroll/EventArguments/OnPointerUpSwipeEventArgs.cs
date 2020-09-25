using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class OnPointerUpSwipeEventArgs : EventArgs
   {
      public Vector2 SwipeDelta { get; }

      public Vector2 SwipeDirection { get; }

      public Vector2 SwipePhaseDistance { get; }

      public OnPointerUpSwipeEventArgs(Vector2 swipeDelta, Vector2 swipeDirection, Vector2 swipePhaseDistance)
      {
         SwipeDelta = swipeDelta;
         SwipeDirection = swipeDirection;
         SwipePhaseDistance = swipePhaseDistance;
      }
   }
}