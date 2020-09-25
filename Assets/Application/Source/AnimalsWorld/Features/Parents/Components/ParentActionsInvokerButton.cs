using System;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   [RequireComponent(typeof(Button))]
   public class ParentActionsInvokerButton : MonoBehaviour
   {
      private Button _buttonComponent = default;
      private IParentActionsInvokerService _parentActionsInvokerService;

      private void Awake()
      {
         _parentActionsInvokerService = new ParentActionsInvokerService();
         _buttonComponent = GetComponent<Button>();
         _buttonComponent.onClick = new Button.ButtonClickedEvent();

         Validation.Run(() => _buttonComponent != null, nameof(_buttonComponent), "Must not be null.");
      }

      private void OnEnable()
      {
         _buttonComponent.onClick.AddListener(_parentActionsInvokerService.Invoke);
      }

      private void OnDisable()
      {
         _buttonComponent.onClick.RemoveAllListeners();
      }
   }
}