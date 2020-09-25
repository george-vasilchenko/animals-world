using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class LevelLockOverlayButton : MonoBehaviour
   {
      private Button _button;

      public void OnEnable()
      {
         _button = _button ?? GetComponent<Button>();
         _button.onClick.AddListener(RequestUnlockLevel);
      }

      public void OnDisable() => _button.onClick.RemoveAllListeners();

      private void RequestUnlockLevel() => GameManager.Instance.PanelManager.SwitchToPanel(GameManager.Instance.PanelManager.ParentPanel);
   }
}