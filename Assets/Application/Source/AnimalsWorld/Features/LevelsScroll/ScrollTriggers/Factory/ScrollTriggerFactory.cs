using System;
using UnityEngine;

namespace AnimalsWorld
{
   public class ScrollTriggerFactory : MonoBehaviour
   {
      public ScrollTriggerBase Create(ScrollTriggerTypeEnum scrollTriggerType, ISwipeHandler swipeHandler, IScrollItemsCollection collection)
      {
         var path = "Prefabs/";
         ScrollTriggerBase prefab;
         ScrollTriggerBase instance;

         switch (scrollTriggerType)
         {
            case ScrollTriggerTypeEnum.SwipeDistanceBased:
               path = string.Concat(path, "SwipeDistanceBasedScrollTrigger");
               prefab = UnityEngine.Resources.Load<SwipeDistanceBasedScrollTrigger>(path);
               instance = Instantiate(prefab, transform, false);

               //((SwipeDistanceBasedScrollTrigger)instance).Initialize(swipeHandler);
               return instance;

            case ScrollTriggerTypeEnum.ItemPositionBased:
               path = string.Concat(path, "ItemPositionBasedScrollTrigger");
               prefab = UnityEngine.Resources.Load<ItemPositionBasedScrollTrigger>(path);
               instance = Instantiate(prefab, transform, false);

               //((ItemPositionBasedScrollTrigger)instance).Initialize(swipeHandler, collection);
               return instance;

            default:
               throw new ArgumentOutOfRangeException(nameof(scrollTriggerType), scrollTriggerType, null);
         }
      }
   }
}