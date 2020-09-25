using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Image))]
   public class PanelBackgroundSwitcher : MonoBehaviour
   {
      [SerializeField] private PanelManager _panelManager = default;
      [SerializeField] private Sprite _mainBackgroundSprite = default;
      [SerializeField] private Sprite _secondaryBackgroundSprite = default;

      private Image _imageComponent;

      private IPanelManager PanelManager => _panelManager;

      private void Awake()
      {
         Validation.Run(() => _panelManager != null, nameof(_panelManager), "Must not be null.");
         Validation.Run(() => _mainBackgroundSprite != null, nameof(_mainBackgroundSprite), "Must not be null.");
         Validation.Run(() => _secondaryBackgroundSprite != null, nameof(_secondaryBackgroundSprite), "Must not be null.");

         _imageComponent = GetComponent<Image>();
         Validation.Run(() => _imageComponent != null, nameof(_imageComponent), "Must not be null.");
      }

      private void Start()
      {
         _imageComponent.sprite = _mainBackgroundSprite;
      }

      private void OnEnable()
      {
         PanelManager.OnPanelChanged += PanelManager_OnPanelChanged;
      }

      private void OnDisable()
      {
         PanelManager.OnPanelChanged -= PanelManager_OnPanelChanged;
      }

      private void PanelManager_OnPanelChanged(OnPanelChangedEventArgs args)
      {
         _imageComponent.sprite = args.PanelTo == PanelManager.StartPanel
            ? _mainBackgroundSprite
            : _secondaryBackgroundSprite;
      }
   }
}