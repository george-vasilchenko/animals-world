using System;
using UnityEngine;

namespace AnimalsWorld
{
   [RequireComponent(typeof(RectTransform))]
   public class ScrollItemsCollection : MonoBehaviour, IScrollItemsCollection
   {
      [SerializeField] private RectTransform[] _collection = default;

      private ScrollItem[] _scrollItems;

      private RectTransform _rectTransform;

      #region IScrollItemsCollection

      public event OnScrollItemsCollectionLengthSetDelegate OnScrollItemsCollectionLengthSet;

      int IScrollItemsCollection.CollectionLength => _collection.Length;

      Vector2 IScrollItemsCollection.GetElementLocalPositionAtIndex(int index)
      {
         if (index < 0 || index > _collection.Length - 1)
         {
            throw new IndexOutOfRangeException("Provided index is out of collection bounds.");
         }

         return _collection[index].localPosition;
      }

      Vector2 IScrollItemsCollection.GetElementWorldPositionAtIndex(int index)
      {
         if (index < 0 || index > _collection.Length - 1)
         {
            throw new IndexOutOfRangeException("Provided index is out of collection bounds.");
         }

         return _collection[index].position;
      }

      Vector2 IScrollItemsCollection.GetAnchoredPosition() => _rectTransform.anchoredPosition;

      Vector2 IScrollItemsCollection.SetAnchoredPosition(Vector2 position)
      {
         _rectTransform.anchoredPosition = position;
         return _rectTransform.anchoredPosition;
      }

      Vector2 IScrollItemsCollection.GetElementScaleAtIndex(int index)
      {
         if (index < 0 || index > _collection.Length - 1)
         {
            throw new IndexOutOfRangeException("Provided index is out of collection bounds.");
         }

         return _collection[index].localScale;
      }

      void IScrollItemsCollection.SetElementScaleAtIndex(int index, Vector2 newScale)
      {
         if (index < 0 || index > _collection.Length - 1)
         {
            throw new IndexOutOfRangeException("Provided index is out of collection bounds.");
         }

         _collection[index].localScale = newScale;
      }

      IScrollItem IScrollItemsCollection.GetScrollItemAtIndex(int index)
      {
         if (index < 0 || index > _scrollItems.Length - 1)
         {
            throw new IndexOutOfRangeException("Provided index is out of collection bounds.");
         }

         return _scrollItems[index];
      }

      #endregion IScrollItemsCollection

      #region Unity API

      public void Awake()
      {
         _rectTransform = GetComponent<RectTransform>();

         if (_collection == null || _collection.Length == 0)
         {
            throw new Exception("Scroll Items Collection must not be empty.");
         }

         if (_rectTransform == null)
         {
            throw new Exception("Rect transform component must not be null.");
         }

         _scrollItems = new ScrollItem[_collection.Length];

         for (var i = 0; i < _collection.Length; i++)
         {
            var scrollItem = _collection[i].GetChild(0).GetComponent<ScrollItem>();

            if (scrollItem == null)
            {
               throw new Exception("Scroll item [" + i + "] in collection is null.");
            }

            scrollItem.SetIndex(i);

            _scrollItems[i] = scrollItem;
         }
      }

      public void Start() => OnScrollItemsCollectionLengthSet?.Invoke(_collection.Length);

      #endregion Unity API
   }
}