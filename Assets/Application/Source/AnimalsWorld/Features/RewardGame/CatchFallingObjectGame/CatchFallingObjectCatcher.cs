using MalenkiyApps;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimalsWorld
{
   [RequireComponent(typeof(BoxCollider2D))]
   public class CatchFallingObjectCatcher : MonoBehaviour, IDragHandler
   {
      public event OnObjectCaughtDelegate OnObjectCaught;

      private BoxCollider2D _collider;
      private RectTransform _rectTransform;

      private void Awake()
      {
         _collider = GetComponent<BoxCollider2D>();
         Validation.Run(() => _collider != null, "Collider", "Must not be null.");

         _rectTransform = GetComponent<RectTransform>();
         Validation.Run(() => _rectTransform != null, "RectTransform", "Must not be null.");
      }

      private void Start() => _collider.size = _rectTransform.sizeDelta;

      private void OnTriggerEnter2D(Component collision)
      {
         if (collision.gameObject != null)
         {
            OnObjectCaught?.Invoke();
         }
      }

      public void OnDrag(PointerEventData eventData)
      {
         var newPostiion = new Vector2(transform.position.x + eventData.delta.x, transform.position.y);
         transform.position = newPostiion;
      }
   }
}