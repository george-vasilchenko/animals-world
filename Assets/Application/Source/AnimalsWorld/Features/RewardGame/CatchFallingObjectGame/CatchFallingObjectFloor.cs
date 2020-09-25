using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   [RequireComponent(typeof(BoxCollider2D))]
   public class CatchFallingObjectFloor : MonoBehaviour
   {
      public event OnObjectLostDelegate OnObjectLost;

      private BoxCollider2D _collider;
      private RectTransform _rectTransform;

      private void Awake()
      {
         _collider = GetComponent<BoxCollider2D>();
         Validation.Run(() => _collider != null, "Collider", "Must not be null.");

         _rectTransform = GetComponent<RectTransform>();
         Validation.Run(() => _rectTransform != null, "RectTransform", "Must not be null.");
      }

      private void Start()
         => _collider.size = new Vector2(Screen.width / GameManager.Instance.UiManager.ScaleFactor,
            _rectTransform.sizeDelta.y);

      private void OnTriggerEnter2D(Component collision)
      {
         if (collision.gameObject != null)
         {
            OnObjectLost?.Invoke();
         }
      }
   }
}