using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Button))]
   public class ValueButtonVisualState : MonoBehaviour
   {
      [SerializeField] private Sprite _activeStateSprite = default;
      [SerializeField] private Sprite _inActiveStateSprite = default;

      private Button _buttonComponent;
      private Image _buttonGraphics;

      private void Awake()
      {
         Validation.Run(() => _activeStateSprite != null, nameof(_activeStateSprite), "Must not be null.");
         Validation.Run(() => _inActiveStateSprite != null, nameof(_inActiveStateSprite), "Must not be null.");

         _buttonComponent = GetComponent<Button>();
         Validation.Run(() => _buttonComponent != null, nameof(_buttonComponent), "Must not be null.");

         _buttonGraphics = _buttonComponent.image;
      }

      public void SetState(bool isActive)
      {
         _buttonGraphics.sprite = isActive
            ? _activeStateSprite
            : _inActiveStateSprite;
      }
   }
}