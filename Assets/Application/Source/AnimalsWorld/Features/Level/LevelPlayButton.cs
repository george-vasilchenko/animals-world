using MalenkiyApps;
using UnityEngine;

namespace AnimalsWorld
{
   public class LevelPlayButton : MonoBehaviour
   {
      public Level TargetLevel;
      public GameObject LevelLockOverlayButtonPrefab;

      private LevelLockOverlayButton _levelLockOverlayButton;

      public void Awake()
      {
         Validation.Run(() => TargetLevel != null, "TargetLevel", "Must not be null.");
         Validation.Run(() => LevelLockOverlayButtonPrefab != null, "LevelLockOverlayButtonPrefab", "Must not be null.");
      }

      public void OnEnable() => RunLockControl();

      private void RunLockControl()
      {
         var isUnlocked =
            GameManager.Instance.LevelManager.IsLevelUnlocked(TargetLevel);

         if (!isUnlocked)
         {
            if (_levelLockOverlayButton == null)
            {
               SpawnLockOverlayButton();
            }
         }
         else
         {
            if (_levelLockOverlayButton != null)
            {
               Destroy(_levelLockOverlayButton.gameObject);
            }
         }
      }

      private void SpawnLockOverlayButton()
      {
         var instance = Instantiate(LevelLockOverlayButtonPrefab, Vector3.zero, Quaternion.identity, transform);

         _levelLockOverlayButton = instance.GetComponent<LevelLockOverlayButton>();

         var instanceRect = _levelLockOverlayButton.GetComponent<RectTransform>();
         instanceRect.anchoredPosition = Vector2.zero;
         instanceRect.offsetMin = Vector2.zero;
         instanceRect.offsetMax = Vector2.zero;
      }
   }
}