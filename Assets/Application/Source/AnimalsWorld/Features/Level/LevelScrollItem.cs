using MalenkiyApps;
using UnityEngine;
using UnityEngine.Events;

namespace AnimalsWorld
{
   public class LevelScrollItem : ScrollItem
   {
      public UnityEvent OnLockClickEvent;
      public Level TargetLevel;
      public GameObject LevelLockOverlayPrefab;

      [SerializeField]
      private bool _isUnlocked;

      private LevelLockOverlay _levelLockOverlay;
      private IParentActionsInvokerService _parentActionsInvokerService;

      #region Unity API

      public void OnEnable() => RunLockControl();

      public void Awake()
      {
         Validation.Run(() => TargetLevel != null, "TargetLevel", "Must not be null.");
         Validation.Run(() => LevelLockOverlayPrefab != null, "LevelLockOverlayPrefab", "Must not be null.");

         _parentActionsInvokerService = new ParentActionsInvokerService();
         OnLockClickEvent.AddListener(_parentActionsInvokerService.Invoke);
      }

      #endregion Unity API

      public override void OnClick()
      {
         if (_isUnlocked)
         {
            OnClickEvent?.Invoke();
         }
         else
         {
            OnLockClickEvent?.Invoke();
         }
      }

      private void RunLockControl()
      {
         _isUnlocked = GameManager.Instance.LevelManager.IsLevelUnlocked(TargetLevel);

         if (!_isUnlocked)
         {
            if (_levelLockOverlay == null)
            {
               SpawnLockOverlayButton();
            }
         }
         else
         {
            if (_levelLockOverlay != null)
            {
               Destroy(_levelLockOverlay.gameObject);
            }
         }
      }

      private void SpawnLockOverlayButton()
      {
         var instance = Instantiate(LevelLockOverlayPrefab, Vector3.zero, Quaternion.identity, transform);

         _levelLockOverlay = instance.GetComponent<LevelLockOverlay>();

         var instanceRect = _levelLockOverlay.GetComponent<RectTransform>();
         instanceRect.anchoredPosition = Vector2.zero;
         instanceRect.offsetMin = Vector2.zero;
         instanceRect.offsetMax = Vector2.zero;
      }
   }
}