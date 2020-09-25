using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class LoadingProgressStepBehaviour : MonoBehaviour, ILoadingProgressStepBehaviour
   {
      [SerializeField] private Sprite _stepTargetSprite = default;
      [SerializeField] private Sprite _stepPlaceholderSprite = default;

      private Image _imageComponent;

      private void Awake()
      {
         _imageComponent = GetComponent<Image>();

         Validation.Run(() => _imageComponent != null, nameof(_imageComponent), "Must not be null.");
         Validation.Run(() => _stepTargetSprite != null, nameof(_stepTargetSprite), "Must not be null.");
         Validation.Run(() => _stepPlaceholderSprite != null, nameof(_stepPlaceholderSprite), "Must not be null.");
      }

      private void Start()
      {
         SetEnabled(false);
      }

      public void SetEnabled(bool isOn)
      {
         if (_imageComponent == null)
         {
            return;
         }

         _imageComponent.sprite = isOn
            ? _stepTargetSprite
            : _stepPlaceholderSprite;
      }
   }
}