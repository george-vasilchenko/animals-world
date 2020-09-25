using System;
using MalenkiyApps;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalsWorld
{
   public class ConfirmationDialog : MonoBehaviour
   {
      [SerializeField] private Button _confirmButton = default;
      [SerializeField] private Button _closeButton = default;
      [SerializeField] private Text _messageText = default;

      private Action _onConfirmAction;
      private Action _onCloseAction;

      public void InitializeDialog(string message, Action onConfirmAction, Action onCloseAction)
      {
         Validation.Run(() => !string.IsNullOrWhiteSpace(message), nameof(message),
            "Must not be null or white space.");
         Validation.Run(() => onConfirmAction != null, nameof(onConfirmAction),
            "Must not be null.");
         Validation.Run(() => onCloseAction != null, nameof(onCloseAction),
            "Must not be null.");

         _messageText.text = message;
         _onConfirmAction = onConfirmAction;
         _onCloseAction = onCloseAction;
      }

      public void OnConfirmHandler()
      {
         _onConfirmAction.Invoke();
         Destroy(gameObject);
      }

      public void OnCloseHandler()
      {
         _onCloseAction.Invoke();
         Destroy(gameObject);
      }

      private void Awake()
      {
         Validation.Run(() => _messageText != null, nameof(_messageText),
            "Must not be null.");
         Validation.Run(() => _confirmButton != null, nameof(_confirmButton),
            "Must not be null.");
         Validation.Run(() => _closeButton != null, nameof(_closeButton),
            "Must not be null.");
      }

      private void OnEnable()
      {
         _confirmButton.onClick.AddListener(OnConfirmHandler);
         _closeButton.onClick.AddListener(OnCloseHandler);
      }

      private void OnDisable()
      {
         _confirmButton.onClick.RemoveAllListeners();
         _closeButton.onClick.RemoveAllListeners();
      }
   }
}