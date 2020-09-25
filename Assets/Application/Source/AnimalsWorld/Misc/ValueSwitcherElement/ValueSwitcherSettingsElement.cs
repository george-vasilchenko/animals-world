using MalenkiyApps;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AnimalsWorld
{
   public class ValueSwitcherSettingsElement : MonoBehaviour, IValueSwitcherSettingsElement
   {
      public event OnValueSwitcherSettingsElementValueChangedDelegate OnValueSwitcherSettingsElementValueChanged;

      public void Initialize(float value)
      {
         var element = _controlButtons.First(o => o.ValueButtonData.Value == value);
         OnControlButtonClickedHandler(element);
      }

      [SerializeField] private ValueSwitcherButton[] _controlButtons = default;

      private void Awake()
      {
         Validation.Run(() => _controlButtons != null, nameof(_controlButtons), "Must not be null.");
         Validation.Run(() => _controlButtons.Any(), nameof(_controlButtons), "Must not be empty.");
      }

      private void OnEnable()
      {
         foreach (var controlButton in _controlButtons)
         {
            controlButton.onClick.AddListener(OnControlButtonClickedAction(controlButton));
         }
      }

      private void OnDisable()
      {
         foreach (var controlButton in _controlButtons)
         {
            controlButton.onClick.RemoveListener(OnControlButtonClickedAction(controlButton));
         }
      }

      private UnityAction OnControlButtonClickedAction(ValueSwitcherButton button) => () =>
         {
            OnControlButtonClickedHandler(button);
         };

      private void OnControlButtonClickedHandler(ValueSwitcherButton button)
      {
         OnValueSwitcherSettingsElementValueChanged?.Invoke(button.ValueButtonData.Value);
         button.ValueButtonVisualState.SetState(isActive: true);

         foreach (var controlButton in _controlButtons)
         {
            if (controlButton != button)
            {
               controlButton.ValueButtonVisualState.SetState(isActive: false);
            }
         }
      }
   }
}