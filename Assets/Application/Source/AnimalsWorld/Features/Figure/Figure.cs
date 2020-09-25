using MalenkiyApps;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimalsWorld
{
   public class Figure : Element, IDragHandler, IPointerDownHandler, IPointerUpHandler
   {
      public event OnFigureSnapDelegate OnSnap;

      public bool IsSnapped { get; private set; }

      public RectTransform AnchorLocation;
      public RectTransform TargetLocation;
      public GameObject ShadowImageObject;

      public bool IsScaleToBeChanged = false;
      public Vector3 TargetScale;

      public const float MinSnapDistancePixels = 30f;

      private RectTransform _elementRectTransform;
      private bool _isSiblingIndSet;
      private Vector3 _originalScale;

      #region Unity API

      public void OnEnable()
      {
         SetShadowVisible(true);
         _isSiblingIndSet = false;
      }

      public void Awake()
      {
         Validation.Run(() => TargetLocation != null, "TargetLocation", "Must not be null.");
         Validation.Run(() => ShadowImageObject != null, "ShadowImageObject", "Must not be null.");
         Validation.Run(() => AnchorLocation != null, "AssignmenAnchorLocationtText", "Must not be null.");

         _elementRectTransform = GetComponent<RectTransform>();
      }

      public void Start() => _originalScale = _elementRectTransform.localScale;

      #endregion Unity API

      #region Drag implementation

      public void OnDrag(PointerEventData eventData)
      {
         if (!_isSiblingIndSet)
         {
            transform.SetAsLastSibling();
            _isSiblingIndSet = true;
         }

         if (!IsSnapped)
         {
            _elementRectTransform.position += (Vector3)eventData.delta;
            IsSnapped = IsSnapDistanceCorrect();

            if (IsSnapped)
            {
               Snap();
               SetShadowVisible(false);
            }
         }
      }

      public void OnPointerDown(PointerEventData eventData)
      {
         if (IsScaleToBeChanged)
         {
            _elementRectTransform.localScale = TargetScale;
         }

         if (!IsSnapped)
         {
            _isSiblingIndSet = false;
         }
      }

      public void OnPointerUp(PointerEventData eventData)
      {
         if (!IsSnapped)
         {
            if (IsScaleToBeChanged)
            {
               _elementRectTransform.localScale = _originalScale;
            }

            SnapToAnchor();
            _isSiblingIndSet = false;
         }
      }

      #endregion Drag implementation

      public void SnapToAnchor()
      {
         _elementRectTransform = _elementRectTransform ?? GetComponent<RectTransform>();
         _elementRectTransform.localPosition = AnchorLocation.localPosition;
      }

      public void ResetElement()
      {
         IsSnapped = false;

         SnapToAnchor();

         _isSiblingIndSet = false;

         OnSnap = null;

         if (IsScaleToBeChanged)
         {
            _elementRectTransform.localScale = _originalScale;
         }
      }

      private void Snap()
      {
         _elementRectTransform.position = TargetLocation.position;

         GameManager.Instance.SoundManager.PlaySnapSound();

         OnSnap?.Invoke(this, new OnFigureSnapEventArgs(gameObject.name));
      }

      private void SetShadowVisible(bool isVisible)
      {
         if (ShadowImageObject.activeSelf != isVisible)
         {
            ShadowImageObject.SetActive(isVisible);
         }
      }

      private bool IsSnapDistanceCorrect()
      {
         var scaleFactor = GameManager.Instance.UiManager.ScaleFactor;
         var distance = Vector2.Distance(_elementRectTransform.position, TargetLocation.position);
         return distance < MinSnapDistancePixels * scaleFactor;
      }
   }
}