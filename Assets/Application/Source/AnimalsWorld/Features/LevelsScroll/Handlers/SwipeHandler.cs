using MalenkiyApps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace AnimalsWorld
{
   public class SwipeHandler : MonoBehaviour, ISwipeHandler,
      IDragHandler, IPointerDownHandler, IPointerUpHandler
   {
      #region ISwipeHandler

      public event OnPointerUpSwipeDelegate OnPointerUpSwipe;

      public event OnSwipePointerDownDelegate OnSwipePointerDown;

      public event OnSwipePointerUpDelegate OnSwipePointerUp;

      public event OnSwipeClickDelegate OnSwipeClick;

      public bool IsSwiping { get; private set; }

      public bool IsPointerDown { get; private set; }

      public Vector2 SwipeInitialDirection { get; private set; }

      public Vector2 SwipeCurrentDirection { get; private set; }

      public float SwipeDeltaHorizontal { get; private set; }

      public float SwipePhaseDistanceHorizontal { get; private set; }

      #endregion ISwipeHandler

      private IUserInterfaceRayCaster UserInterfaceRayCaster => _userInterfaceRayCaster;

      [SerializeField] private UserInterfaceRayCaster _userInterfaceRayCaster = default;

      [SerializeField] private Canvas _canvas = default;

      [SerializeField] private SwipeHandlerData _data;

      private void ResetDragData()
      {
         IsSwiping = false;
         SwipePhaseDistanceHorizontal = 0;
         SwipeDeltaHorizontal = 0;
         SwipeInitialDirection = Vector2.zero;
         SwipeCurrentDirection = Vector2.zero;
         _data.LastMousePosition = CrossPlatformInputManager.mousePosition;
      }

      private void OnPointerHeldDown()
      {
         SwipeDeltaHorizontal = Mathf.Abs((CrossPlatformInputManager.mousePosition - _data.LastMousePosition).x);

         IsSwiping = SwipeDeltaHorizontal > 1;

         _data.LastMousePosition = CrossPlatformInputManager.mousePosition;
      }

      #region Unity API

      public void Awake()
      {
         Validation.Run(() => UserInterfaceRayCaster != null, nameof(UserInterfaceRayCaster), "Must not be null.");
         Validation.Run(() => _canvas != null, nameof(_canvas), "Must not be null.");
      }

      public void Update()
      {
         if (IsPointerDown)
         {
            //Debug.LogFormat("held: {0}", SwipeDeltaHorizontal);
            OnPointerHeldDown();
         }
      }

      public void Start()
      {
         _data.Initialize(gameObject);
         UserInterfaceRayCaster.Initialize();
      }

      public void OnDrag(PointerEventData eventData)
      {
         SwipeCurrentDirection = new Vector2((int)Mathf.Sign(eventData.delta.x), (int)Mathf.Sign(eventData.delta.y));

         if (SwipeInitialDirection.magnitude <= 0)
         {
            SwipeInitialDirection = SwipeCurrentDirection;
         }

         SwipePhaseDistanceHorizontal += SwipeDeltaHorizontal;
      }

      public void OnPointerDown(PointerEventData eventData)
      {
         //Debug.LogFormat("down");
         OnSwipePointerDown?.Invoke();
         IsPointerDown = true;
         ResetDragData();
      }

      public void OnPointerUp(PointerEventData eventData)
      {
         OnSwipePointerUp?.Invoke();
         OnPointerUpSwipe?.Invoke(new OnPointerUpSwipeEventArgs(new Vector2(SwipeDeltaHorizontal, 0),
            SwipeCurrentDirection, new Vector2(SwipePhaseDistanceHorizontal, 0)));

         if (SwipePhaseDistanceHorizontal < _data.ClickSensitivity * _canvas.scaleFactor)
         {
            //Debug.Log();

            UserInterfaceRayCaster.Project(CrossPlatformInputManager.mousePosition, out var resultsList);
            OnSwipeClick?.Invoke(resultsList);
         }

         IsPointerDown = false;
         ResetDragData();
      }

      #endregion Unity API
   }
}