using UnityEngine;

namespace AnimalsWorld
{
   public interface IScrollItemsCollection
   {
      event OnScrollItemsCollectionLengthSetDelegate OnScrollItemsCollectionLengthSet;

      int CollectionLength { get; }

      Vector2 GetElementWorldPositionAtIndex(int index);

      Vector2 GetElementLocalPositionAtIndex(int index);

      Vector2 GetAnchoredPosition();

      Vector2 SetAnchoredPosition(Vector2 position);

      Vector2 GetElementScaleAtIndex(int index);

      void SetElementScaleAtIndex(int index, Vector2 newScale);

      IScrollItem GetScrollItemAtIndex(int index);
   }
}